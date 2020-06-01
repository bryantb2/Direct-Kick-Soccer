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
        public Province Providence { get; set; }
        public String CorporatePageURL { get; set; }
        public String BusinessEmail { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsApproved { get; set; }

        // methods
        public String GenerateRequestParagraph()
        {
            return "Request to build team " + TeamName + ", described as " + TeamDescription + ". " +
                "Business is located in " + Providence + ", " + Country + ". Company can be reached at " +
                BusinessEmail + " or by phone at " + PhoneNumber + ". Here is the business main site: "
                + CorporatePageURL;
        }
    }
}
