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
        private IProductCategoryRepo categoryRepo;
        private IProductGroupRepo productGroupRepo;

        public ProductController(IRosterProductRepo rosterProductRepo,
            ICustomProductRepo customProductRepo,
            IProductSortRepo sortRepo,
            IProductCategoryRepo categoryRepo,
            IProductGroupRepo productGroupRepo)
        {
            teamRepo = tRepo;
            this.rosterProductRepo = rosterProductRepo;
            this.customProductRepo = customProductRepo;
            this.sortRepo = sortRepo;
            this.categoryRepo = categoryRepo;
            this.productGroupRepo = productGroupRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View("Search", null);
        }

        public async Task<IActionResult> ViewProduct(int productGroupId)
        {
            // get product group
            var foundGroup = productGroupRepo.GetGroupById(productGroupId);

            // setup view model
            var viewProductVM = new ProductSelectionViewModel()
            {
                ProductGroup = foundGroup
            };

            // send to view
            return View(viewProductVM);
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
            var foundGroups = SearchHelper.SearchByString<ProductGroup>(productGroupRepo.Groups, searchString);

            // create browse view model
            var browseVM = SearchHelper.CreateBrowseObject<ProductGroup>(
                    currentPage == -1 ? 0 : currentPage,
                    searchTerm: searchString,
                    queriedGroups: foundGroups);

            // return view
            return View("Search", browseVM);
        }
        public async Task<IActionResult>ViewTeamProduct(int teamId)
        {
            Team t = await teamRepo.FindTeamById(teamId);
            return View(t);
        }
        public async Task<IActionResult>TeamProdDetails(int id)
        {
            CustomProduct prod = await customProductRepo.GetCustomProductById(id);
            return View(prod);
        }
        public async Task<IActionResult> DisplayByCategory(int categoryId, int currentPage = -1)
        {
            // get products by category
            var categoryGroups = SearchHelper.FilterByCategory<ProductGroup>(productGroupRepo.Groups, categoryId);

            // get current category
            var category = categoryRepo.GetCategoryById(categoryId);

            // create browse view model
            var browseVM = SearchHelper.CreateBrowseObject<ProductGroup>(
                currentPage == -1 ? 0 : currentPage,
                productCategory: category,
                queriedGroups: categoryGroups);

            // return view
            return View("Search", browseVM);
        }

        public async Task<IActionResult> SortProductGroups(int sortId, int categoryId = -1, string searchTerm = null, int currentPage = -1)
        {
            // IMPORTANT: at no point will the user be allowed to search AND browse by category AT THE SAME TIME

            // get products by appropriate query
            var searchableList = productGroupRepo.Groups;
            var filteredGroups = categoryId != -1 ? 
                SearchHelper.FilterByCategory<ProductGroup>(searchableList, categoryId) 
                    : SearchHelper.SearchByString<ProductGroup>(searchableList, searchTerm);

            // get sort and check sort type
            var foundSort = sortRepo.GetSortById(sortId);
            var productGroupSortArgument = 0;
            if(foundSort.SortName.ToUpper() == "LOWEST PRICE")
            {
                productGroupSortArgument = -1;
            }
            else if(foundSort.SortName.ToUpper() == "HIGHEST PRICE")
            {
                productGroupSortArgument = 1;
            }
            SearchHelper.SortGroupsByPrice(ref filteredGroups, sortBy: productGroupSortArgument);

            // create browse view model
            BrowseViewModel browseVM = null;
            if (categoryId != -1)
            {
                // means user is browsing by category
                var foundCategory = categoryRepo.GetCategoryById(categoryId);
                browseVM = SearchHelper.CreateBrowseObject<ProductGroup>(
                    currentPage == -1 ? 0 : currentPage,
                    productCategory: foundCategory,
                    queriedGroups: filteredGroups);
            }
            else
            {
                // user is browsing products THEY searched
                browseVM = SearchHelper.CreateBrowseObject<ProductGroup>(
                    currentPage == -1 ? 0 : currentPage,
                    searchTerm: searchTerm,
                    queriedGroups: filteredGroups);
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
        // private actions
        private BrowseViewModel CreateBrowseObject(List<CustomProduct> queriedProducts, int currentPageNumber, ProductCategory categoryObj = null, string searchTerm = null)
        {
            // setup starting and ending product indexes
            var itemsPerPage = 30;
            var startProduct = currentPageNumber * itemsPerPage;
            var endProduct = startProduct + 30;

            // setup paging view model
            var pagingInfo = new BrowseViewModel()
            {
                Products = SplitList(queriedProducts, startProduct, endProduct),
                CurrentPage = currentPageNumber,
                SearchString = searchTerm == null ? null : searchTerm,
                CurrentCategory = categoryObj == null ? null : categoryObj,
                // next page exists if the number of products left in the query is greater than the total number of dispalyed products
                NextPageExists = queriedProducts.Count > endProduct ? true : false,
                PreviousPageExists = startProduct - itemsPerPage > 0 ? true : false
            };

            return pagingInfo;
        }

        private List<CustomProduct> SplitList(List<CustomProduct> filterableList, int start, int end)
        {
            // remember: index is one behind the actual product number in the list
            if (filterableList.Count == 0)
                return filterableList;

            // check if end parameter is higher than remain filterable list count (prevent out of range error
            var checkedEnd = (filterableList.Count - end) < 0 ? filterableList.Count : end;
            var splitList = new List<CustomProduct>();
            for(var i = start; i <= checkedEnd - 1; i++)
            {
                splitList.Add(filterableList[i]);
            }
            return splitList;
        }

        private List<CustomProduct> FilterProductsByCategory(int categoryId)
        {
            var filteredProducts = customProductRepo.CustomProducts.Where(product => product.BaseProduct.Category.ProductCategoryID == categoryId);
            return filteredProducts.ToList();
        }

        private List<CustomProduct> SearchByString(string searchString)
        {
            if(searchString.Length >= 2)
            {
                // clean search term
                var cleanedSearchTerm = searchString.Trim().Split(' ');
                // checks product tags, title, color, size, SKU, model number
                var customProducts = customProductRepo.CustomProducts;
                var foundProducts = new List<CustomProduct>();
                foreach (var product in customProducts)
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
            return new List<CustomProduct>();
        }

        private bool DoesQueryContainString(string[] query, string stringToCheck)
        {
            var stringAsTolken = stringToCheck.Split(' ');
            foreach (var searchTerm in query)
            {
                foreach(var checkAgainstTerm in stringAsTolken)
                {
                    if (searchTerm.ToUpper() == checkAgainstTerm.ToUpper() || checkAgainstTerm.ToUpper().Contains(searchTerm.ToUpper()))
                        return true;
                }
            }
            return false;
        }

        private bool DoesQueryContainString(string[] query, List<Tag> tagsToCheck)
        {
            if (tagsToCheck == null)
                return false;
            else
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

        

        public async Task<IActionResult> CreateCustomProd(RosterProduct prod, List<Tag> tags, string title, string description,
                                                    string imageUrl, bool isActive, decimal price)
        {
            PricingHistory myPrice = new PricingHistory
            {
                DateChanged = DateTime.Now,
                NewPrice = price
            };
            CustomProduct myProd = new CustomProduct
            {
                BaseProduct=prod,
                ProductTitle=title,
                ProductDescription=description,
                CustomImagePNG=imageUrl,
                IsProductActive=isActive,
                
                
            };
            myProd.AddPricingHistory(myPrice);
            foreach(Tag t in tags)
            {
                myProd.AddTag(t);
            }
            customProductRepo.AddCustomProduct(myProd);

            return View();
        }
    }
}
