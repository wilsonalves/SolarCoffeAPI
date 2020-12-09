using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.ViewModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(32)] public string FirstName { get; set; }
        [MaxLength(32)] public string lastname { get; set; }

        public CustomerAddressmodel PrimaryAddress { get; set; }


        public class CustomerAddressmodel
        {
            public int Id { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime UpdateOn { get; set; }

            [MaxLength(100)]
            public string AddressLine1 { get; set; }
            [MaxLength(100)]
            public string AddressLine2 { get; set; }
            [MaxLength(100)]
            public string City { get; set; }
            [MaxLength(2)]
            public string State { get; set; }
            [MaxLength(12)]
            public string PostalCode { get; set; }
            [MaxLength(60)]
            public string Country { get; set; }

        }
    }
}