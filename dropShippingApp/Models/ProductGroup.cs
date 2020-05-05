using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProductGroup
    {
        public int ProductGroupID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string GeneralThumbnail { get; set; }
        public List<CustomProduct> ChildProducts { get; set; }
    }
}
