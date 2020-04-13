using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductSize
    {
        public int ProductSizeID { get; set; }
        public bool IsSizeActive { get; set; }
        public string SizeName { get; set; }
    }
}
