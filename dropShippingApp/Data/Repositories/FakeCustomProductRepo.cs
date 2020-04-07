using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
	public class FakeCustomProductRepo : ICustomProductRepo
	{
        private List<CustomProduct> customProducts = new List<CustomProduct>();
        public List<CustomProduct> CustomProducts { get { return customProducts; } }

        // methods
        public async Task AddCustomProduct(CustomProduct newProduct)
        {
            CustomProducts.Add(newProduct);
        }

        public async Task<CustomProduct> GetCustomProductById(int customProductId)
        {
            CustomProduct foundProduct = CustomProducts.Find(product => product.CustomProductID == customProductId);
            if(foundProduct != null)
            {
                return await Task.FromResult<CustomProduct>(foundProduct);
            }
            // Return the custom product as null if not found
            return await Task.FromResult<CustomProduct>(null);
        }

        public async Task UpdateCustomProduct(CustomProduct updatedProduct)
        {
            CustomProduct oldProduct = CustomProducts.Find(cp => cp.CustomProductID == updatedProduct.CustomProductID);
            CustomProducts.Remove(oldProduct);
            CustomProducts.Add(updatedProduct);
        }

        public async Task RemoveCustomProduct(CustomProduct product)
        {
            if(product != null)
            {
                CustomProducts.Remove(product);
            }
        }

        //Needs to be moved to colorRepo
        //public void AddColor(ProductColor color) => OfferedColors.Add(color);
        //public ProductColor RemoveColor(ProductColor color)
        //{
        //    ProductColor removedColor = null;
        //    foreach (ProductColor c in OfferedColors)
        //    {
        //        if (c.ProductColorID == color.ProductColorID)
        //        {
        //            removedColor = c;
        //            OfferedColors.Remove(c);
        //            return removedColor;
        //        }
        //    }
        //    return removedColor;
        //}

        ////Needs to be moved to sizeRepo
        //public void AddSize(ProductSize size) => OfferedSizes.Add(size);
        //public ProductSize RemoveSize(ProductSize size)
        //{
        //    ProductSize removedSize = null;
        //    foreach (ProductSize s in OfferedSizes)
        //    {
        //        if (s.ProductSizeID == size.ProductSizeID)
        //        {
        //            removedSize = s;
        //            OfferedSizes.Remove(s);
        //            return removedSize;
        //        }
        //    }
        //    return removedSize;
        //}

        ////Needs to be moved to productRepo
        //public void AddPriceChange(PricingHistory historyLog) => PricingHistory.Add(historyLog);
        //public PricingHistory RemovePriceChange(PricingHistory historyLog)
        //{
        //    // Commented out until DB is set up
        //    PricingHistory removedLog = null;
        //    foreach (PricingHistory hist in PricingHistory)
        //    {
        //        if (hist.PricingHistoryID == historyLog.PricingHistoryID)
        //        {
        //            removedLog = hist;
        //            PricingHistory.Remove(hist);
        //            return removedLog;
        //        }
        //    }
        //    return removedLog;
        //}

        //public void AddCustomImage(string image)
        //{
        //    // Not implemented yet
        //    throw new NotImplementedException();
        //}

        //public void RemoveCustomImage(string image)
        //{
        //    // Not implemented yet
        //    throw new NotImplementedException();
        //}
    }
}
