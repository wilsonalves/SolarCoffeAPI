using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Order
{
   public interface IOrderService
    {
        List<SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateOpenOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);

    }
}
