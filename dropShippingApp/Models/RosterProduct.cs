using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class RosterProduct
    {
        // private fields
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();

        // public properties
        public int RosterProductID { get; set; }
        public bool IsProductActive { get; set; }
        [Required]
        public int SKU { get; set; }
        [Required]
        public ProductColor BaseColor { get; set; }
        [Required]
        public ProductSize BaseSize { get; set; }
        public List<PricingHistory> PricingHistory { get { return this.pricingHistory; } }
        public RosterGroup RosterGroup { get; set; }

        // methods
        public decimal CurrentPrice
        {
            get
            {
                PricingHistory highestDate = null;
                foreach (var pricing in PricingHistory)
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

        public void AddPricingHistory(PricingHistory history) => pricingHistory.Add(history);
        public PricingHistory RemovePricingHistory(int historyId)
        {
            var removedHistory = this.pricingHistory.Find(hstry => hstry.PricingHistoryID == historyId);
            this.pricingHistory.Remove(removedHistory);
            return removedHistory;
        }
    }
}
