using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
	public class FakeLocationRepo : ILocationRepo
	{
		private List<Country> countries = new List<Country>();
		private List<Province> provinces = new List<Province>();
		public List<Country> Countries { get { return countries; } }
		public List<Province> Provinces { get { return provinces; } }

		public List<Country> GetAllCountries => Countries;

		public List<Province> GetAllProvinces => Provinces;

		public async Task AddCountry(Country newCountry)
		{
			Countries.Add(newCountry);
		}

		public async Task AddProvince(Province newProvince)
		{
			Provinces.Add(newProvince);
		}

		public async Task RemoveCountry(Country country)
		{
			if (country != null)
			{
				Countries.Remove(country);
			}
		}

		public async Task RemoveProvince(Province province)
		{
			if (province != null)
			{
				Provinces.Add(province);
			}
		}

		public async Task UpdateCountry(Country updatedCountry)
		{
			Country oldCountry = Countries.Find(c => c.CountryID == updatedCountry.CountryID);
			Countries.Remove(oldCountry);
			Countries.Add(updatedCountry);
		}

		public async Task UpdateProvince(Province updatedProvince)
		{
			Province oldProvince = Provinces.Find(p => p.ProvinceID == updatedProvince.ProvinceID);
			Provinces.Remove(oldProvince);
			Provinces.Add(updatedProvince);
		}
	}
}
