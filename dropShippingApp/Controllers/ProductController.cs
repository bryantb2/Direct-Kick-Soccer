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

namespace dropShippingApp.Controllers
{
    public class ProductController : Controller
    {
        private IRosterProductRepo rosterProductRepo;
        private ICustomProductRepo customProductRepo;
        private ISortRepo sortRepo;
        public int PageSize=30//num of prod per page


        public ProductController(IRosterProductRepo rosterProductRepo,
            ICustomProductRepo customProductRepo,
            ISortRepo sortRepo)
        {
            this.rosterProductRepo = rosterProductRepo;
            this.customProductRepo = customProductRepo;
            this.sortRepo = sortRepo;
        }

        public async Task<IActionResult> Index()
        {
            //IQueryable<PricingHistory> result = await Repository.GetAllPriceHistAsync();
            //return View(result.ToList());
            return View();
        }


        public async Task<IActionResult> PopularItems()
        {
            // TODO
            // returns team results page 
            return View();
        }

        public async Task<IActionResult> Search(string searchString, int productPage = 1) 
        {
            // 
            var csProduct = customProductRepo.CustomProducts;
           
                         
            if (!String.IsNullOrEmpty(searchString))
            {
                csProduct = csProduct.Where(s => s.BaseProduct.Category.Name == searchString).OrderBy(p => p.CustomProductID)
                    .Skip((productPage-1)*PageSize)
                    .Take(PageSize)
                    .ToList();   
            }
               return View(csProduct); 
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
        
        [HttpGet]
        public async Task<IActionResult> SortView()
        {
            List<CustomProduct> prods = (from p in customProductRepo.CustomProducts
                                         select p).ToList();
            return View(prods);
        }
        
        [HttpPost]
        public async Task<IActionResult> SortView(string searchTerm, int sortId)
        {
            // get sort object and products from search
            var foundSort = sortRepo.GetSortById(sortId);
            var foundProducts = SearchByString(searchTerm);

            // perform sort
            foundProducts.Sort(foundSort.SortOperation);

            // return list
            return View("Search",foundProducts);
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
