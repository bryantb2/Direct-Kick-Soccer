using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string PaypalPurchaseId { get; set; }
        public string PaypalInvoiceId { get; set; }
        public string PaypalOrderId { get; set; }
        public string SETrackingId { get; set; }
        public string SEReturnTrackingId { get; set; }
        public bool IsApproved { get; set; }
    }
}
