using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class CustomProduct
    {
        // private fields
        private List<ProductColor> offeredColors = new List<ProductColor>();
        private List<ProductSize> offeredSizes = new List<ProductSize>();
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();

        // public properties
        public int CustomProductID { get; set; }
        public int SKU { get; set; }
        public RosterProduct BaseProduct { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public decimal AddOnPrice { get; set; }
        public string CustomImagePNG { get; set; }
        public string CustomImageSVG { get; set; }
        public bool IsProductActive { get; set; }
    }
}
