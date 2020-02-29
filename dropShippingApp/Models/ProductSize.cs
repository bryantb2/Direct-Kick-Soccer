using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductSize
    {
        public int ProductSizeID { get; set; }
        public String SizeName { get; set; }
        public decimal AddOnPrice { get; set; }
        public bool IsSizeActive { get; set; }
    }
}
