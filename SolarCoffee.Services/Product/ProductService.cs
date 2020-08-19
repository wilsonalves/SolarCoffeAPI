using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;
        public ProductService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
      

        /// <summary>
        /// Adds a new product to the databse
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10

                };
                _db.ProductInventorys.Add(newInventory);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSucess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Error saving Product",
                    IsSucess = false
                };

               
            }
            
            

        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// get produc by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Product GetProductById(int id)
        {
            //return _db.Products.Where(x => x.Id == id).FirstOrDefault();
            return _db.Products.Find(id);
        }
        /// <summary>
        /// Archives a Product by setting boolean IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public  ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived product",
                    IsSucess = true
                };

            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSucess = false
                };

                
            }
        }
    }
}
