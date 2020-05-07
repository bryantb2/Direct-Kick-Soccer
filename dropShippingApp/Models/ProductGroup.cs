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
        public List<Tag> ProductTags { get; set; }

        public decimal GetHighestPrice
        {
            get
            {
                var highestPrice = 0m;
                foreach(var product in ChildProducts)
                {
                    if (product.CurrentPrice > highestPrice)
                        highestPrice = product.CurrentPrice;
                }
                return highestPrice;
            }
        }

        public decimal GetLowestPrice
        {
            get
            {
                var lowestPrice = ChildProducts[0].CurrentPrice;
                foreach (var product in ChildProducts)
                {
                    if (product.CurrentPrice < lowestPrice)
                        lowestPrice = product.CurrentPrice;
                }
                return lowestPrice;
            }
        }
    }
}
