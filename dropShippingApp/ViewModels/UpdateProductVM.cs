using dropShippingApp.APIModels;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Http;
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
        public IFormFile PhotoData { get; set; }
        public CustomProduct ProductData { get; set; }
        public decimal? CurrentPrice { get; set; }
        public string LinkToImage { get; set; }
    }
}
