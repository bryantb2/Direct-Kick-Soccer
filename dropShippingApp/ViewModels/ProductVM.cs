using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class ProductVM
    {
        // houses product info for display 
        public int ProductGroupId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GeneralThumbnail { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductSize Size { get; set; }
        public ProductColor Color { get; set; }
    }
}
