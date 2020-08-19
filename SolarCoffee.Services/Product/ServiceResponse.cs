using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Product
{
    public class ServiceResponse<T>
    {
        public bool IsSucess { get; set; }
        public string Message {get;set;}
        public DateTime Time { get; set; }
        public T Data { get; set; }
    }
}
