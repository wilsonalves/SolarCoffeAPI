using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }

        public ProductInventory GetByProductId(int productID)
        {
            throw new NotImplementedException();
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            throw new NotImplementedException();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ProductInventory> UpdatedUnitsAvailable(int id, int adjustment)
        {
            throw new NotImplementedException();
        }
    }
}
