using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;
using dropShippingApp.Data;
using dropShippingApp.Models;
using dropShippingApp.Data.Repositories;
using dropShippingApp.ViewModels;
using Microsoft.Xrm.Sdk.Query;

namespace dropShippingApp.Controllers
{
    public class ProductController : Controller
    {
        private IRosterProductRepo rosterProductRepo;
        private ICustomProductRepo customProductRepo;
        private ISortRepo sortRepo;
        private ICategoryRepo categoryRepo;

        public ProductController(IRosterProductRepo rosterProductRepo,
            ICustomProductRepo customProductRepo,
            ISortRepo sortRepo,
            ICategoryRepo categoryRepo)
        {
            this.rosterProductRepo = rosterProductRepo;
            this.customProductRepo = customProductRepo;
            this.sortRepo = sortRepo;
            this.categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View("Search", null);
        }


        public async Task<IActionResult> PopularItems()
        {
            // TODO
            // returns team results page 
            return View();
        }

        public async Task<IActionResult> Search(string searchString, int currentPage = -1) 
        {
            // search for products
            var foundProducts = SearchByString(searchString);
            var availableSorts = sortRepo.Sorts;

            // setup paging view model
            var pagingInfo = new BrowseViewModel()
            {
                Sorts = availableSorts,
                Products = foundProducts,
                ItemsPerPage = 30,
                CurrentPage = (currentPage == -1 ? 0 : currentPage),
                SearchString = searchString,
                CurrentCategory = null
            };

            // return
            return View("Search", pagingInfo);
        }

        public async Task<IActionResult> DisplayByCategory(int categoryId, int currentPage = -1)
        {
            // get products by category
            var filteredProducts = FilterProductsByCategory(categoryId);
            var availableSorts = sortRepo.Sorts;

            // get current category
            var category = await categoryRepo.GetCategoryById(categoryId);

            // setup paging view model
            var pagingInfo = new BrowseViewModel()
            {
                Sorts = availableSorts,
                Products = filteredProducts,
                ItemsPerPage = 30,
                CurrentPage = (currentPage == -1 ? 0 : currentPage),
                SearchString = null,
                CurrentCategory = category
            };

            // return
            return View("Search", pagingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> SortProducts(int sortId, int categoryId = -1, string searchTerm = null, int currentPage = -1)
        {
            // IMPORTANT: at no point will the user be allowed to search AND browse by category AT THE SAME TIME
            var availableSorts = sortRepo.Sorts;
            var filteredProducts = new List<CustomProduct>();
            if (categoryId != -1)
            {
                filteredProducts = FilterProductsByCategory(categoryId);
            }
            else
            {
                filteredProducts = SearchByString(searchTerm);
            }

            // get sort object and products from search
            var foundSort = sortRepo.GetSortById(sortId);

            // check sort type
            if(foundSort.SortName.ToUpper() == "LOWEST PRICE")
            {
                filteredProducts.Sort((product1, product2) => product2.CurrentPrice.CompareTo(product1.CurrentPrice));
            }
            else if(foundSort.SortName.ToUpper() == "HIGHEST PRICE")
            {
                filteredProducts.Sort((product1, product2) => product1.CurrentPrice.CompareTo(product2.CurrentPrice));
            }

            // setup paging view model
            var pagingInfo = new BrowseViewModel()
            {
                Sorts = availableSorts,
                Products = filteredProducts,
                ItemsPerPage = 30,
                CurrentPage = (currentPage == -1 ? 0 : currentPage),
                SearchString = (searchTerm == null ? null : searchTerm),
                CurrentCategory = (categoryId != -1 ? await categoryRepo.GetCategoryById(categoryId) : null)
            };

            // return list
            return View("Search", pagingInfo);
        }

        public async Task<IActionResult> GetProductBySKU(int SKU)
        {
            var foundProduct = customProductRepo.CustomProducts
                .Find(product => product.BaseProduct.SKU == SKU);

            // add admin view at some point to browse products
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetProductsByModelNumber(int modelNumber)
        {
            var foundProducts = customProductRepo.CustomProducts
                .Where(product => product.BaseProduct.ModelNumber == modelNumber);

            // add admin view at some point to browse products
            throw new NotImplementedException();
        }

        public async Task<IActionResult> ViewProduct(int productId)
        {
            // get product
            var foundProduct = await customProductRepo.GetCustomProductById(productId);

            var productViewModel = new ProductViewModel
            {
                Product = foundProduct,
                Quantity = 1
            };

            // send to view
            return View(productViewModel);
        }

        private List<CustomProduct> FilterProductsByCategory(int categoryId)
        {
            var filteredProducts = customProductRepo.CustomProducts.Where(product => product.BaseProduct.Category.ProductCategoryID == categoryId);
            return filteredProducts.ToList();
        }

        private List<CustomProduct> SearchByString(string searchString)
        {
            // clean search term
            var cleanedSearchTerm = searchString.Trim().Split(' ');
            // checks product tags, title, color, size, SKU, model number
            var customProducts = customProductRepo.CustomProducts;
            var foundProducts = new List<CustomProduct>();
            foreach(var product in customProducts)
            {
                if (DoesQueryContainString(cleanedSearchTerm, product.ProductTitle))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.ProductTags))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.ProductTags))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.ModelNumber.ToString()))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.SKU.ToString()))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.BaseColor.ColorName))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.BaseSize.SizeName))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.ProductTags))
                    foundProducts.Add(product);
                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.Category.Name))
                    foundProducts.Add(product);
            }
            return foundProducts;
        }

        private bool DoesQueryContainString(string[] query, string stringToCheck)
        {
            foreach(var term in query)
            {
                if (term.ToUpper() == stringToCheck.ToUpper())
                    return true;
            }
            return false;
        }

        private bool DoesQueryContainString(string[] query, string[] stringsToCheck)
        {
            foreach (var term in query)
            {
                foreach(var checkAgainstTerm in stringsToCheck)
                {
                    if (term.ToUpper() == checkAgainstTerm.ToUpper())
                        return true;
                }
            }
            return false;
        }

        private bool DoesQueryContainString(string[] query, List<Tag> tagsToCheck)
        {
            foreach (var term in query)
            {
                foreach (var tag in tagsToCheck)
                {
                    if (term.ToUpper() == tag.TagLine.ToUpper())
                        return true;
                }
            }
            return false;
        }
    }
}
