using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
	public interface IProprietaryCollectionRepo
	{
		// CRUD operations for Proprietary Collection
		Task CreateNewCollection(ProprietaryCollection newCollection);
		List<ProprietaryCollection> GetAllCollections { get; }
		Task<ProprietaryCollection> GetCollectionByName(string categoryName);
		Task UpdateCollection(ProprietaryCollection updatedCollection);
		Task RemoveCollection(ProprietaryCollection collection);
	}
}
