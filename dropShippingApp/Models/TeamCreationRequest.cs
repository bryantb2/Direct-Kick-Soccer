using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class TeamCreationRequest
    {
        // public properties
        public int TeamCreationRequestID { get; set; }
        public String TeamName { get; set; }
        public String TeamDescription { get; set; }
        public String StreetAddress { get; set; }
        public String ZipCode { get; set; }
        public Country Country { get; set; }
        public String CorporatePageURL { get; set; }
        public String BusinessEmail { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsApproved { get; set; }
    }
}
