using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
	public interface ILocationRepo
	{
		// Crud OPS for Country
		Task AddCountry(Country newCountry);
		List<Country> Countries { get; }
		Task UpdateCountry(Country updatedCountry);
		Task RemoveCountry(Country country);
	

		// Crud OPS for Province
		Task AddProvince(Province newProvince);
		List<Province> Provinces { get; }
		Task UpdateProvince(Province updatedProvince);
		Task RemoveProvince(Province province);
	}
}
