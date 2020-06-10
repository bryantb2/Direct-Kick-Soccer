using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
	public interface ICustomProductRepo
	{
		List<CustomProduct> CustomProducts { get; }
		Task AddCustomProduct(CustomProduct newProduct);
		CustomProduct GetCustomProductById(int customProductId);
 		Task UpdateCustomProduct(CustomProduct updatedProduct);
		Task<CustomProduct> RemoveCustomProduct(int productId);
	}
}
