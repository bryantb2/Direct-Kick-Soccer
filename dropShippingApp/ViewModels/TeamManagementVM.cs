using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class TeamManagementVM
    {
        public int LifeTimeSales { get; set; }
        public int MonthlySales { get; set; }
        public int WeeklySales { get; set; }
        public Team Team { get; set; }
        public List<RosterGroupVM> OfferedProductGroups { get; set; }
    }
}
