using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductCategory : Category
    {
        [Key]
        public int ProductCategoryID { get; set; }
    }
}
