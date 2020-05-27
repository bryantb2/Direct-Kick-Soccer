using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class CustomGroupSelectionCardVM
    {
        public List<ProductGroup> ProductGroups { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ButtonText { get; set; }
        public string TitleText { get; set; }

        // these are for sending data up to the controller
        public int SelectedGroupID { get; set; }
    }
}
