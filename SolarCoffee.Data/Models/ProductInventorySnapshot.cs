﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
   public class ProductInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapShotTime { get; set; }
        public DateTime QuantityOnHand { get; set; }
        public Product Product { get; set; }
    }
}
