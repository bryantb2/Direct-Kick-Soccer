using dropShippingApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class CreateProductVM
    {
        public int GroupId { get; set; }
        public IFormFile ProductPhoto { get; set; }
        public decimal InitialPrice { get; set; }
        public int SelectedBaseProduct { get; set; }
        public List<RosterProduct> AvailableBaseProducts { get; set; }
    }
}
