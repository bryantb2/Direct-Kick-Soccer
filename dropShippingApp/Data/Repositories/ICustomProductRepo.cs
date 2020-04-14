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
		// CRUD operations for CustomProducts
		Task AddCustomProduct(CustomProduct newProduct);
		Task<CustomProduct> GetCustomProductById(int customProductId);
 		Task UpdateCustomProduct(CustomProduct updatedProduct);
		Task RemoveCustomProduct(CustomProduct product);

		// May implement at later date
		//Task<List<CustomProduct>> GetCustomProductsByTeam(Team team);
	}
}
