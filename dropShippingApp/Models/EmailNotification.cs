using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class EmailNotification
    {
        public int EmailNotificiationID { get; set; }
        public String EmailSubject { get; set; }
        public String EmailBody { get; set; }
        public AppUser Recipient { get; set; }
        public Invoice InvoiceCopy { get; set; }
        public String OrderUpdate { get; set; } // example: "Your order has shipped!"
    }
}
