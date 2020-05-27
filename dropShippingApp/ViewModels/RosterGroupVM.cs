using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    // this groups all the products of a single product group together
    public class RosterGroupVM
    {
        public List<RosterProduct> ProductList { get; set; }
    }
}
