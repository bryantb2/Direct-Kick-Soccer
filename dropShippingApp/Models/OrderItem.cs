using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public string ProductFamilyID { get; set; }
        public string ProductID { get; set; }
        public string TeamID { get; set; }
    }
}
