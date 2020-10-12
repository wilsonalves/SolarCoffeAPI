using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Order
{
  public  class OrderService : IOrderService
    {
        private readonly SolarCoffee.Data.SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _iventoryService;


        public OrderService(SolarCoffee.Data.SolarDbContext db, ILogger<OrderService> logger, IProductService productService, IInventoryService inventoryService)
        {
            _db = db;
            _logger = logger;
            _iventoryService = inventoryService;
            _productService = productService;


        }
        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            var now = DateTime.UtcNow;

            _logger.LogInformation("Generating new order");

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService
                    .GetProductById(item.Product.Id);
                var inventoryId = _iventoryService
                    .GetByProductId(item.Product.Id).Id;
                _iventoryService
                    .UpdatedUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSucess = true,
                    Data = true,
                    Message = "Open order created",
                    Time = now
                };
            }

            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSucess = false,
                    Data = false,
                    Message = e.StackTrace,
                    Time = now
                };
            }
        }
        /// <summary>
        /// get all SalersOrders in the system
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders.Include(so => so.Customer).
                ThenInclude(so=>so.PrimaryAddress).
                Include(so => so.SalesOrderItems).ThenInclude(item=>item.Product)
                .ToList();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = now;
            order.IsPaid = true;

            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSucess = true,
                    Data = true,
                    Message = $" Order ${order.Id} closed : invoice paid in full",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSucess = false,
                    Data = false,
                    Message = e.StackTrace,
                    Time = now
                };
              
            }
        }
    }
}
