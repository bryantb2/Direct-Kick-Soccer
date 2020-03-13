using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductColor
    {
        public int ProductColorID { get; set; }
        public string ColorName { get; set; }
        public decimal AddOnPrice { get; set; }
        public bool IsColorActive { get; set; }
    }
}
