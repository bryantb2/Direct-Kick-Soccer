using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class CustomProduct
    {
        // private fields
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();
        private List<Tag> tags = new List<Tag>();

        // public properties
        public int CustomProductID { get; set; }
        public ImgurPhotoData ProductPhotoData { get; set; }
        [Required]
        public bool IsProductActive { get; set; }
        [Required]
        public RosterProduct BaseProduct { get; set; } // parent <------
        public List<PricingHistory> PricingHistory { get { return this.pricingHistory; } }

        public decimal CurrentPrice
        {
            get
            {
                PricingHistory highestDate = null;
                foreach(var pricing in PricingHistory)
                {
                    // set initial value
                    if (highestDate == null)
                        highestDate = pricing;
                    else
                        // set highest pricing date val to current one in loop if higher
                        if (highestDate.DateChanged < pricing.DateChanged)
                            highestDate = pricing;
                }
                return highestDate.NewPrice;
            }
        }

        public decimal GetPriceAtTimeOfSale(DateTime saleDate)
        {
            for (var i = PricingHistory.Count - 1; i >= 0; i++)
            {
                var currentHistory = pricingHistory[i];
                if (saleDate >= currentHistory.DateChanged)
                    return currentHistory.NewPrice;
            }
            return 0m;
        }

        // methods
        public void AddPricingHistory(PricingHistory history) => pricingHistory.Add(history);
        public PricingHistory RemovePricingHistory(int historyId)
        {
            var removedHistory = this.pricingHistory.Find(hstry => hstry.PricingHistoryID == historyId);
            this.pricingHistory.Remove(removedHistory);
            return removedHistory;
        }
        public void AddTag(Tag t) => tags.Add(t);
    }
}
