using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
	public class FakeProprietaryCollectionRepo : IProprietaryCollectionRepo
	{
		private List<ProprietaryCollection> collections = new List<ProprietaryCollection>();
		public List<ProprietaryCollection> Collections { get { return collections; } }

		public async Task CreateNewCollection(ProprietaryCollection newCollection)
		{
			Collections.Add(newCollection);
		}

		public List<ProprietaryCollection> GetAllCollections
		{
			get
			{
				return Collections;
			}
		}

		public async Task<ProprietaryCollection> GetCollectionByName(string categoryName)
		{
			ProprietaryCollection foundCollection = Collections.Find(name => name.CategoryName == categoryName);
			if (foundCollection != null)
			{
				return await Task.FromResult<ProprietaryCollection>(foundCollection);
			}
			return await Task.FromResult<ProprietaryCollection>(null);
		}

		public async Task UpdateCollection(ProprietaryCollection updatedCollection)
		{
			ProprietaryCollection oldCollection = Collections.Find(c => c.ProprietaryCollectionID == updatedCollection.ProprietaryCollectionID);
			Collections.Remove(oldCollection);
			Collections.Add(updatedCollection);
		}

		public async Task RemoveCollection(ProprietaryCollection collection)
		{
			Collections.Remove(collection);
		}
	}
}
