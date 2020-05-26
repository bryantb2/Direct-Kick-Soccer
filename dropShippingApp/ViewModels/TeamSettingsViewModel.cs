using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.ViewModels
{
	public class TeamSettingsViewModel
	{
        public int TeamID { get; set; }
        public String Name { get; set; }
        public Country Country { get; set; }
        public Province Providence { get; set; }
        public String StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public String CorporatePageURL { get; set; }
        public String BusinessEmail { get; set; }
        public string PhoneNumber { get; set; }
        public String Description { get; set; }
    }
}
