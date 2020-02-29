using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class CustomProduct
    {
        // private fields
        private List<ProductColor> offeredColors = new List<ProductColor>();
        private List<ProductSize> offeredSizes = new List<ProductSize>();
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();

        // public properties
        public int CustomProductID { get; set; }
        public int SKU { get; set; }
        public RosterProduct BaseProduct { get; set; }
        public String ProductTitle { get; set; }
        public String ProductDescription { get; set; }
        public decimal AddOnPrice { get; set; }
        public String CustomImagePNG { get; set; }
        public String CustomImageSVG { get; set; }
        public bool IsProductActive { get; set; }

        // methods
        public void AddColor(ProductColor color) => offeredColors.Add(color);
        public ProductColor RemoveColor(ProductColor color)
        {
            ProductColor removedColor = null;
            foreach (ProductColor c in offeredColors)
            {
                if(c.ProductColorID == color.ProductColorID)
                {
                    removedColor = c;
                    offeredColors.Remove(c);
                    return removedColor;
                }
            }
            return removedColor;
        }

        public void AddSize(ProductSize size) => offeredSizes.Add(size);
        public ProductSize RemoveSize(ProductSize size)
        {
            ProductSize removedSize = null;
            foreach (ProductSize s in offeredSizes)
            {
                if (s.ProductSizeID == size.ProductSizeID)
                {
                    removedSize = s;
                    offeredSizes.Remove(s);
                    return removedSize;
                }
            }
            return removedSize;
        }

        public void AddPriceChange(PricingHistory historyLog) => pricingHistory.Add(historyLog);
        public PricingHistory RemovePriceChange(PricingHistory historyLog)
        {
            PricingHistory removedLog = null;
            foreach (PricingHistory hist in pricingHistory)
            {
                if (hist.PricingHistoryID == historyLog.PricingHistoryID)
                {
                    removedLog = hist;
                    pricingHistory.Remove(hist);
                    return removedLog;
                }
            }
            return removedLog;
        }
    }
}
