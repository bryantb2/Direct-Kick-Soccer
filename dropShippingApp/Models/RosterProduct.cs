using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class RosterProduct
    {
        // private fields
        private List<PricingHistory> pricingHistory = new List<PricingHistory>;
        
        // public properties
        public int RosterProductID { get; set; }
        public int ModelNumber { get; set; }
        public int SKU { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public ProductColor BaseColor { get; set; }
        public ProductSize BaseSize { get; set; }
        public decimal BasePrice { get; set; }
        public decimal AddOnPrice { get; set; }
        public bool IsProductActive { get; set; }
        public List<PricingHistory> PricingHistory { get { return this.pricingHistory; } }

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
