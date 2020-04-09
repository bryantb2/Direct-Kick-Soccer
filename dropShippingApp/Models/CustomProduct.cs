using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class CustomProduct
    {
        // private fields
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();

        // public properties
        public int CustomProductID { get; set; }
        public RosterProduct BaseProduct { get; set; } // parent <------
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public string CustomImagePNG { get; set; }
        public string CustomImageSVG { get; set; }
        public bool IsProductActive { get; set; }
        public List<PricingHistory> PricingHistory { get { return this.pricingHistory; } } // <---- Users set this
        // Does the pricing history INCLUDE the basePrice from the "parent"?

        // methods
        public void AddPricingHistory(PricingHistory history) => pricingHistory.Add(history);
        public PricingHistory RemovePricingHistory(int historyId)
        {
            var removedHistory = this.pricingHistory.Find(hstry => hstry.PricingHistoryID == historyId);
            this.pricingHistory.Remove(removedHistory);
            return removedHistory;
        }
    }
}
