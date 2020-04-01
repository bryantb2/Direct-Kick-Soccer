using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class CustomProduct
    {
        // private fields
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();

        private List<ProductColor> OfferedColors { get; set; }
        private List<ProductSize> OfferedSizes { get; set; }
        private List<PricingHistory> PricingHistory { get; set; }

        // public properties
        public int CustomProductID { get; set; }
    }
}
