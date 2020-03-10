using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class InvoiceItem
    {
        // public properties
        public int InvoiceItemID { get; set; }
        public CustomProduct PurchasedProduct { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public int ItemQuantity { get; set; }

        // methods
        public decimal CalulateSubtotal(DateTime purchaseDate)
        {
            // use pricing history of purchased product to determine what the price was
            var pricingHistory = PurchasedProduct.PricingHistory
                .OrderBy(hist=>hist.DateChanged).ToList(); // put history list into ascending order
            PricingHistory correctHistory = null;
            for(var i = (pricingHistory.Count - 1); i >= 0; i--)
            {
                if(correctHistory != null) // prevents val from being set again
                {
                    var currentHist = pricingHistory[i];
                    if (currentHist.DateChanged <= purchaseDate)
                    {
                        correctHistory = currentHist;
                    }
                }
            }
            return (ItemQuantity * correctHistory.NewPrice);

            // explaination: for loop goes in reverse order, from largest to smallest date values in pricing history
                // will find the one that has a date changed that is less than the purchase data
                // ability to set pricing history is locked after first less-than DateTime is found
                // NOTE: I reversed the pricing history order because, otherwise, you would get the first value in history as the selected date EVERYTIME
        }
    }
}
