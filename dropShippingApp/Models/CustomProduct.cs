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

        // public properties
        public int CustomProductID { get; set; }
        [Required]
        public RosterProduct BaseProduct { get; set; } // parent <------
        public List<Tag> ProductTags { get; set; }
        [Required]
        public string ProductTitle { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public string CustomImagePNG { get; set; }
        public string CustomImageSVG { get; set; }
        [Required]
        public bool IsProductActive { get; set; }
        [Required]
        public List<PricingHistory> PricingHistory { get { return this.pricingHistory; } } // <---- Users set this
                                                                                           // Does the pricing history INCLUDE the basePrice from the "parent"?

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
