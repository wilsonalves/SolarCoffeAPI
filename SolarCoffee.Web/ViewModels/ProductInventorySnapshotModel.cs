﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.ViewModels
{
    public class ProductInventorySnapshotModel
    {
        public List<int> QuantityOnHand { get; set; }
        public int ProductId { get; set; }


        /// <summary>
        /// Snapshot history in format suitable for graphing
        /// </summary>
        public class SnapshotResponse
        {
            public List<ProductInventorySnapshotModel> ProductInventorySnapshots { get; set; }
            public List<DateTime> Timeline { get; set; }
        }
    }
}
