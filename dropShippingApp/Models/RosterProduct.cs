using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class RosterProduct
    {
        // private fields
<<<<<<< Updated upstream
        private List<ProductColor> baseColors = new List<ProductColor>();
        private List<ProductSize> baseSizes = new List<ProductSize>();

=======
        private List<PricingHistory> pricingHistory = new List<PricingHistory>();
        
>>>>>>> Stashed changes
        // public properties
        public int RosterProductID { get; set; }
        public int ModelNumber { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public List<ProductColor> BaseColors { get { return baseColors; } }
        public List<ProductSize> BaseSizes { get { return baseSizes; } }
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
