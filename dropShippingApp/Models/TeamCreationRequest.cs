using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class TeamCreationRequest
    {
        // public properties
        public int RequestID { get; set; }
        public String TeamName { get; set; }
        public String TeamDescription { get; set; }
        public String BusinessAddress { get; set; }
        public String BusinessZipCode { get; set; }
        public Country Country { get; set; }
        public String CorporateSiteURL { get; set; }
        public String BusinessEmail { get; set; }
        public bool IsApproved { get; set; }
    }
}
