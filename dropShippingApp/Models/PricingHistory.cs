using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class PricingHistory
    {
        public int PricingHistoryID { get; set; }
        public DateTime DateChanged { get; set; }
        public decimal NewPrice { get; set; }
    }
}
