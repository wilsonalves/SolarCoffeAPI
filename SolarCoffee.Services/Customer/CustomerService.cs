using Microsoft.EntityFrameworkCore;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly Data.SolarDbContext _db;
        public CustomerService(Data.SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        /// <summary>
        ///  create a new customer; criando cliente
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSucess = true,
                    Message = " New record add",
                    Time = DateTime.Now,
                    Data = customer
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSucess = false,
                    Message = ex.StackTrace,
                    Time = DateTime.Now,
                    Data = customer
                };

                throw;
            }
        }
        /// <summary>
        /// deletar cliente
        /// </summary>
        /// <param name="id"> id do usuario/cliente</param>
        /// <returns> service response bool</returns>
        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;
            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSucess = false,
                    Message = "customer excluded"
                };

            }
            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    IsSucess = true,
                    Message = " customer removed!",
                    Time = DateTime.Now,
                    Data = true
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSucess = false,
                    Message = ex.StackTrace,
                    Data= false
                };

               
            }
        }

        public List<Data.Models.Customer> GetAllCustomer()
        {
            return _db.Customers.Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.FirstName)
                .ToList();
        }

        /// <summary>
        /// get record by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Customer GetById(int id)
        {
            var customer = _db.Customers.Find(id);
            return customer;
        }
            

    }
}
