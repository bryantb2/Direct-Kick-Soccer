using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class CustomProductSelectionCardVM
    {
        public List<ProductGroup> ProductGroups { get; set; }
        public string TitleText { get; set; }
        public string ButtonText { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        // these are for sending data up to the 
        public int SelectedGroupID { get; set; }
        public int SelectedProductID { get; set; }
    }
}
