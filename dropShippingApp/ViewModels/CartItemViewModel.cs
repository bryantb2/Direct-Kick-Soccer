using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class CartItemViewModel
    {
        public CartItem CartItem { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public string GeneralThumbnail { get; set; }
    }
}
