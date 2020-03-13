using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Repositories
{
    public class CustomProductRepo
    {
        // need to implement a DBContext
        //private AppDbContext context;

        // Not implemented yet...
        //public List<ProductColor> OfferedColors { get { return context.OfferedColors.ToList(); } }
        //public List<ProductSize> OfferedSizes { get { return context.OfferedSizes.ToList(); } }
        //public List<PricingHistory> PricingHistory { get { return context.PricingHistory.ToList(); } }
        public List<ProductColor> offeredColors = new List<ProductColor>();
        public List<ProductSize> offeredSizes = new List<ProductSize>();
        public List<PricingHistory> pricingHistory = new List<PricingHistory>();

        public List<ProductColor> OfferedColors { get { return offeredColors; } }
        public List<ProductSize> OfferedSizes { get { return offeredSizes; } }
        public List<PricingHistory> PricingHistory { get { return pricingHistory; } }

        // methods
        public void AddColor(ProductColor color) => OfferedColors.Add(color);
        public ProductColor RemoveColor(ProductColor color)
        {
            //Commented out until DB is set up
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
            //throw new NotImplementedException();
        }

        public void AddSize(ProductSize size) => OfferedSizes.Add(size);
        public ProductSize RemoveSize(ProductSize size)
        {
            // Commented out until DB is set up
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
            //throw new NotImplementedException();
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
            //throw new NotImplementedException();
        }

        public CustomProduct AddCustomImage(CustomProduct Image)
        {
            throw new NotImplementedException();
        }

        public CustomProduct RemoveCustomImage(CustomProduct Image)
        {
            throw new NotImplementedException();
        }

        public CustomProduct AddCustomProduct(CustomProduct product)
        {
            throw new NotImplementedException();
        }

        public CustomProduct RemoveCustomProduct(CustomProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
