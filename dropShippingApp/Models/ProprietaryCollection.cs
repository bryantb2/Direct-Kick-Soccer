using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ProprietaryCollection
    {
        // private fields
        private List<CustomProduct> categoryProducts = new List<CustomProduct>();

        // public properties
        public int ProprietaryCollectionID { get; set; }
        public String CategoryName { get; set; }
        public String CategoryDescription { get; set; }

        // methods
        public void AddCustomProduct(CustomProduct product) => categoryProducts.Add(product);
        public CustomProduct RemoveCustomProduct(CustomProduct product)
        {
            CustomProduct removedProduct = null;
            foreach (CustomProduct p in categoryProducts)
            {
                if (p.CustomProductID == product.CustomProductID)
                {
                    removedProduct = p;
                    categoryProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
        }
    }
}
