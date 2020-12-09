﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController( ILogger<OrderController> logger, IOrderService orderService,
            ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }
        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
        {
            _logger.LogInformation("Generating invoice by wilson alves");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            _orderService.GenerateOpenOrder(order);
            return Ok();


        }
        [HttpGet("/api/order")]
        public ActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            var orderModels = OrderMapper.SerializeOrdersToViewmodels(orders);
            return Ok(orderModels);
        }


        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            _logger.LogInformation("Marking order {id} complete ...");

            _orderService.MarkFulfilled(id);
           
            return Ok();
        }
    }
}
