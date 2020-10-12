using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly Data.SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(Data.SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _logger = logger;
            _db = dbContext;
        }
        private void CreateSnapshot(ProductInventory inventory)
        {
            var now = DateTime.UtcNow;
            var snapshot = new ProductInventorySnapshot
            {
                SnapShotTime = now,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _db.Add(snapshot);
           
        }

      

        /// <summary>
        /// Get a product for ID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public ProductInventory GetByProductId(int productID)
        {
            return _db.ProductInventorys.Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productID);
        }
        /// <summary>
        /// Return all curent iventory database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventorys.Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// return snapshot history the previous 6 hrs
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots.Include(snap => snap.Product)
                  .Where(snap => snap.SnapShotTime > earliest && !snap.Product.IsArchived).ToList();
        }
        /// <summary>
        /// updates number of units aalable of the provided product
        /// </summary>
        /// <param name="id">productid</param>
        /// <param name="adjustment"> number of units added/ removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdatedUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventorys.Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);

                }
                catch (Exception e)
                {

                    _logger.LogError(" Error creating iventory snapshot");
                    _logger.LogError(e.StackTrace);
                }
                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSucess = true,
                    Message = " Product Iventory adjusted",
                    Time = DateTime.Now,
                    Data = inventory
                };


            }
            catch (Exception ex)
            {

                return new ServiceResponse<ProductInventory>
                {
                    IsSucess = false,
                    Message = ex.StackTrace,
                    Time = DateTime.Now,
                    Data = null
                };

            }
        }
    }
}




