using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class BrowseViewModel
    {
        public string SearchString { get; set; }
        public int CurrentPage { get; set; }
        public bool NextPageExists { get; set; }
        public bool PreviousPageExists { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
        public ProductCategory CurrentProductCategory { get; set; }
        public TeamCategory CurrentTeamCategory { get; set; }
        public List<Team> Teams { get; set; }
    }
}
