using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _customerService = customerService;
            _logger = logger;
        }
        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("creating a new customer");
            customer.CreatedOn = DateTime.Now;
            customer.UpdatedOn = DateTime.Now;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomer()
        {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAllCustomer();
            var customerModels = customers.Select(customer => new CustomerModel
            {   Id = customer.Id,
                FirstName = customer.FirstName,
                lastname = customer.LastName,
                PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn
            }).OrderByDescending(customer => customer.CreatedOn).ToList();
            return Ok(customerModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("delete a customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
    }
}
