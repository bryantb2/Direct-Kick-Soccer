using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
	public class FakeCustomProductRepo //: ICustomProductRepo
	{
        /* private List<CustomProduct> customProducts = new List<CustomProduct>();
         public List<CustomProduct> CustomProducts { get { return customProducts; } }

         // methods
         public async Task AddCustomProduct(CustomProduct newProduct)
         {
             CustomProducts.Add(newProduct);
         }

         public List<CustomProduct> GetAllCustomProducts => CustomProducts;

         public async Task<CustomProduct> GetCustomProductById(int customProductId)
         {
             CustomProduct foundProduct = CustomProducts.Find(product => product.CustomProductID == customProductId);
             if (foundProduct != null)
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
             if (product != null)
             {
                 CustomProducts.Remove(product);
             }
         }*/
    }
}
