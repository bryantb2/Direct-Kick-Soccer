using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ActivityLog
    {
        // public properties
        public int ActivityLogID { get; set; }
        public String Title { get; set; } // example: "Added product" 
        public String ChangeDescription { get; set; } // example: "red sweatshirt with small, medium, and large sizes"
        public DateTime TimeStamp { get; set; } 

        // methods
        public String LogToString()
        {
            return (Title + ", " + ChangeDescription + " on " + TimeStamp.ToString());
        }
    }
}
