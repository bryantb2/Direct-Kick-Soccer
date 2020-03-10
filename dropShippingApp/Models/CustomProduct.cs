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
        public RosterProduct BaseProduct { get; set; }
        public String ProductTitle { get; set; }
        public String ProductDescription { get; set; }
        public String CustomImagePNG { get; set; }
        public String CustomImageSVG { get; set; }
        public bool IsProductActive { get; set; }

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
