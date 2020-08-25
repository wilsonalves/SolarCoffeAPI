using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
   public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdatedUnitsAvailable(int id, int adjustment);

        public ProductInventory GetByProductId(int productID);
        public void CreateSnapshot();

        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
