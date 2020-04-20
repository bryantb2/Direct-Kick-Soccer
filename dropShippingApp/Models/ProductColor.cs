using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductColor 
    {
        public int ProductColorID { get; set; }
        public bool IsColorActive { get; set; }
        [Required]
        public string ColorName { get; set; }
    }
}
