using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public List<Data.Models.Customer> GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        public Data.Models.Customer GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
