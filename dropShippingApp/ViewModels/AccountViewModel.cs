using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class AccountViewModel
    {
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNum { get; set; }
        public List<PayPalCheckoutSdk.Orders.Order> OrderList { get; set; }
    }
}
