﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class Customer    {

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime Firsname { get; set; }
        public DateTime LastName { get; set; }
        public CustomerAdresse PrimaryAddress { get; set; }
    }
}
