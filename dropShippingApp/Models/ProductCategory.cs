using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductCategory
    {
        public int ProductCategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        //[Required]
        //[MaxLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        //public string BriefDescription { get; set; }
    }
}
