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
        public bool IsRosterProduct { get; set; }
        public bool IsCustomProduct { get; set; }
        public CustomProduct CustomProduct { get; set; }
        public RosterProduct RosterProduct { get; set; }
    }
}
