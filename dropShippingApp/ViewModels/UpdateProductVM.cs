using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class UpdateProductVM
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public string ProductName { get; set; }
        public decimal? CurrentPrice { get; set; }
        [Required]
        [MinLength(5)]
        public string ProductImageURL { get; set; }
    }
}
