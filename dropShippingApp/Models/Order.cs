using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string PaypalOrderId { get; set; }
        public string SETrackingId { get; set; }
        public string SEReturnTrackingId { get; set; }
        public bool ReturnRequested { get; set; }
        public List<string> ProductFamilyIDs { get; set; }
        public List<string> TeamIDs { get; set; }
    }
}
