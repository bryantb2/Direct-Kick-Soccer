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
using dropShippingApp.HelperUtilities;

namespace dropShippingApp.Controllers
{
    public class ProductController : Controller
    {
        private IRosterProductRepo rosterProductRepo;
        private ICustomProductRepo customProductRepo;
        private IProductSortRepo sortRepo;
        private ICategoryRepo categoryRepo;

        public ProductController(IRosterProductRepo rosterProductRepo,
            ICustomProductRepo customProductRepo,
            IProductSortRepo sortRepo,
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

        public async Task<IActionResult> BackToFirstPage(int categoryId = -1, string searchTerm = null)
        {
            if (searchTerm != null)
                return RedirectToAction("Search", new
                {
                    searchString = searchTerm,
                    currentPage = 0
                });
            else
                return RedirectToAction("DisplayByCategory", new
                {
                    categoryId = categoryId,
                    currentPage = 0
                });
        }

        public async Task<IActionResult> NextPage(int currentPage, int categoryId = -1, string searchTerm = null)
        {
            if (searchTerm != null)
                return RedirectToAction("Search", new
                {
                    searchString = searchTerm,
                    currentPage = currentPage + 1
                });
            else
                return RedirectToAction("DisplayByCategory", new
                {
                    categoryId = categoryId,
                    currentPage = currentPage + 1
                });
        }

        public async Task<IActionResult> PreviousPage(int currentPage, int categoryId = -1, string searchTerm = null)
        {
            if (searchTerm != null)
                return RedirectToAction("Search", new
                {
                    searchString = searchTerm,
                    currentPage = currentPage - 1
                });
            else
                return RedirectToAction("DisplayByCategory", new
                {
                    categoryId = categoryId,
                    currentPage = currentPage - 1
                });
        }

        public async Task<IActionResult> Search(string searchString, int currentPage = -1) 
        {
            // search for products
            var foundProducts = SearchHelper.SearchByString<CustomProduct>(customProductRepo.CustomProducts, searchString);

            // create browse view model
            var browseVM = SearchHelper.CreateBrowseObject(
                    currentPage == -1 ? 0 : currentPage,
                    searchTerm: searchString,
                    queriedProducts: foundProducts);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> DisplayByCategory(int categoryId, int currentPage = -1)
        {
            // get products by category
            var categoryProducts = SearchHelper.FilterByCategory<CustomProduct>(
                customProductRepo.CustomProducts,
                categoryId);

            // get current category
            var category = categoryRepo.GetCategoryById(categoryId);

            // create browse view model
            var browseVM = SearchHelper.CreateBrowseObject(
                currentPage == -1 ? 0 : currentPage,
                categoryObj: category,
                queriedProducts: categoryProducts);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> SortProducts(int sortId, int categoryId = -1, string searchTerm = null, int currentPage = -1)
        {
            // IMPORTANT: at no point will the user be allowed to search AND browse by category AT THE SAME TIME
            // get products by appropriate query
            var searchableList = customProductRepo.CustomProducts;
            var filteredProducts = categoryId != -1 ? SearchHelper.FilterByCategory<CustomProduct>(searchableList, categoryId) : SearchHelper.SearchByString<CustomProduct>(searchableList, searchTerm);

            // get sort and check sort type
            var foundSort = sortRepo.GetSortById(sortId);
            if(foundSort.SortName.ToUpper() == "LOWEST PRICE")
            {
                filteredProducts.Sort((product1, product2) => product1.CurrentPrice.CompareTo(product2.CurrentPrice));
            }
            else if(foundSort.SortName.ToUpper() == "HIGHEST PRICE")
            {
                filteredProducts.Sort((product1, product2) => product2.CurrentPrice.CompareTo(product1.CurrentPrice));
            }

            // create browse view model
            BrowseViewModel browseVM = null;
            if (categoryId != -1)
            {
                // means user is browsing by category
                var foundCategory = categoryRepo.GetCategoryById(categoryId);
                browseVM = SearchHelper.CreateBrowseObject(
                    currentPage == -1 ? 0 : currentPage,
                    categoryObj: foundCategory,
                    queriedProducts: filteredProducts);
            }
            else
            {
                // user is browsing products THEY searched
                browseVM = SearchHelper.CreateBrowseObject(
                    currentPage == -1 ? 0 : currentPage,
                    searchTerm: searchTerm,
                    queriedProducts: filteredProducts);
            }

            // return list
            return View("Search", browseVM);
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
    }
}
