using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Repositories
{
	public class FakeCustomProductRepo : ICustomProductRepo
	{
        // need to implement a DBContext
        //private AppDbContext context;

        // Not implemented yet...
        //public List<ProductColor> OfferedColors { get { return context.OfferedColors.ToList(); } }
        //public List<ProductSize> OfferedSizes { get { return context.OfferedSizes.ToList(); } }
        //public List<PricingHistory> PricingHistory { get { return context.PricingHistory.ToList(); } }
        private List<ProductColor> offeredColors = new List<ProductColor>();
        private List<ProductSize> offeredSizes = new List<ProductSize>();
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();
        private List<CustomProduct> customProducts = new List<CustomProduct>();

        public List<ProductColor> OfferedColors { get { return offeredColors; } }
        public List<ProductSize> OfferedSizes { get { return offeredSizes; } }
        public List<PricingHistory> PricingHistory { get { return pricingHistory; } }
        public List<CustomProduct> CustomProducts { get { return customProducts; } }

        // methods
        public void AddColor(ProductColor color) => OfferedColors.Add(color);
        public ProductColor RemoveColor(ProductColor color)
        {
            ProductColor removedColor = null;
            foreach (ProductColor c in OfferedColors)
            {
                if (c.ProductColorID == color.ProductColorID)
                {
                    removedColor = c;
                    OfferedColors.Remove(c);
                    return removedColor;
                }
            }
            return removedColor;
        }

        public void AddSize(ProductSize size) => OfferedSizes.Add(size);
        public ProductSize RemoveSize(ProductSize size)
        {
            ProductSize removedSize = null;
            foreach (ProductSize s in OfferedSizes)
            {
                if (s.ProductSizeID == size.ProductSizeID)
                {
                    removedSize = s;
                    OfferedSizes.Remove(s);
                    return removedSize;
                }
            }
            return removedSize;
        }

        public void AddPriceChange(PricingHistory historyLog) => PricingHistory.Add(historyLog);
        public PricingHistory RemovePriceChange(PricingHistory historyLog)
        {
            // Commented out until DB is set up
            PricingHistory removedLog = null;
            foreach (PricingHistory hist in PricingHistory)
            {
                if (hist.PricingHistoryID == historyLog.PricingHistoryID)
                {
                    removedLog = hist;
                    PricingHistory.Remove(hist);
                    return removedLog;
                }
            }
            return removedLog;
        }

        public void AddCustomImage(string image)
        { 
            // Not implemented yet
            throw new NotImplementedException();
        }

        public void RemoveCustomImage(string image)
        {
            // Not implemented yet
            throw new NotImplementedException();
        }

        public void AddCustomProduct(CustomProduct product)
        {
            CustomProducts.Add(product);
        }

        public void RemoveCustomProduct(CustomProduct product)
        {
            CustomProducts.Remove(product);
        }
    }
}
