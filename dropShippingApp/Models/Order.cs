using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public String PackageTrackingLink { get; set; }
        public bool HasShipped { get; set; }
    }
}
