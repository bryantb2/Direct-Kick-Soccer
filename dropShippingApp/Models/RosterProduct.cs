using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class RosterProduct
    {
        // private fields
        private List<ProductColor> baseColors = new List<ProductColor>();
        private List<ProductSize> baseSizes = new List<ProductSize>();

        // public properties
        public int RosterProductID { get; set; }
        public int ModelNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        // Adjusted lists for BaseColors and BaseSizes so they can read/write
        public List<ProductColor> BaseColors { get; set; }
        public List<ProductSize> BaseSizes { get; set; }
        public decimal BasePrice { get; set; }
        public decimal AddOnPrice { get; set; }

        // methods
        public void AddColor(ProductColor color) => baseColors.Add(color);
        public ProductColor RemoveColor(ProductColor color)
        {
            ProductColor removedColor = null;
            foreach(ProductColor c in baseColors)
            {
                if(c.ProductColorID == color.ProductColorID)
                {
                    removedColor = c;
                    baseColors.Remove(c);
                    return removedColor;
                }
            }
            return removedColor;
        }

        public void AddSize(ProductSize size) => baseSizes.Add(size);
        public ProductSize RemoveSize(ProductSize size)
        {
            ProductSize removedSize = null;
            foreach (ProductSize s in baseSizes)
            {
                if (s.ProductSizeID == size.ProductSizeID)
                {
                    removedSize = s;
                    baseSizes.Remove(s);
                    return removedSize;
                }
            }
            return removedSize;
        }
    }
}
