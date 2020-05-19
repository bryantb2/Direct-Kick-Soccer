using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
	public class RealLocationRepo : ILocationRepo
	{
		private ApplicationDbContext context;

		public RealLocationRepo(ApplicationDbContext appDbContext)
		{
			this.context = appDbContext;
		}

		public List<Country> Countries { get { return this.context.Countries.ToList(); } }
		public List<Province> Provinces { get { return this.context.Provinces.ToList(); } }

		public async Task AddCountry(Country newCountry)
		{
			context.Countries.Add(newCountry);
			await context.SaveChangesAsync();
		}

		public async Task RemoveCountry(Country country)
		{
			context.Countries.Remove(country);
			await context.SaveChangesAsync();
		}

		public async Task UpdateCountry(Country updatedCountry)
		{
			context.Countries.Update(updatedCountry);
			context.Countries.Add(updatedCountry);
		}

		public async Task AddProvince(Province newProvince)
		{
			context.Provinces.Add(newProvince);
			await context.SaveChangesAsync();
		}

		public async Task UpdateProvince(Province updatedProvince)
		{
			context.Provinces.Update(updatedProvince);
			await context.SaveChangesAsync();
		}

		public async Task RemoveProvince(Province province)
		{
			context.Provinces.Remove(province);
			await context.SaveChangesAsync();
		}
	}
}
