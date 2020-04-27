using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.ViewModels
{
	public class ProductCategoriesViewModel
	{
		public List<ProductCategory> Categories { get; set; }
	}
}
