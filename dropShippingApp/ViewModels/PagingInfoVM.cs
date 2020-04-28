using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class BrowseViewModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPge { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

        public string SearchString { get; set; }
        public List<CustomProduct> products { get; set; }
    }
}
