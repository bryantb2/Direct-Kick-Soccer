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
        [Required]
        public decimal BasePrice { get; set; } // <--- Raza sets this
        public bool IsProductActive { get; set; }
        [Required]
        public int ModelNumber { get; set; }
        [Required]
        public int SKU { get; set; }
        [Required]
        public ProductColor BaseColor { get; set; }
        [Required]
        public ProductSize BaseSize { get; set; }
        public List<Tag> ProductTags { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
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
