using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
	public class RealProprietaryCollectionRepo : IProprietaryCollectionRepo
	{
		private ApplicationDbContext context;
		public List<ProprietaryCollection> Collections { get { return context.ProprietaryCollections.ToList(); } }

		public RealProprietaryCollectionRepo(ApplicationDbContext appDbContext)
		{
			context = appDbContext;
		}

		public async Task CreateNewCollection(ProprietaryCollection newCollection)
		{
			context.ProprietaryCollections.Add(newCollection);
			await context.SaveChangesAsync();
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
			ProprietaryCollection foundCollection = context.ProprietaryCollections.FirstOrDefault(name => name.CategoryName == categoryName);
			if (foundCollection != null)
			{
				return await Task.FromResult<ProprietaryCollection>(foundCollection);
			}
			return await Task.FromResult<ProprietaryCollection>(null);
		}

		public async Task UpdateCollection(ProprietaryCollection updatedCollection)
		{
			this.context.ProprietaryCollections.Update(updatedCollection);
			await context.SaveChangesAsync();
		}

		public async Task RemoveCollection(ProprietaryCollection collection)
		{
			this.context.ProprietaryCollections.Remove(collection);
			await context.SaveChangesAsync();
		}
	}
}
