using dropShippingApp.Models;
using System;
using System.Collections.Generic;

namespace dropShippingApp.ViewModels
{
    public class BrowseViewModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

        public string SearchString { get; set; }
        public List<CustomProduct> CustomProducts { get; set; }
        public string CurrentCategory { get; set; }

        public IEnumerable<RosterProduct> Products;
        public BrowseViewModel PagingInfo { get; set; }
    }
}
