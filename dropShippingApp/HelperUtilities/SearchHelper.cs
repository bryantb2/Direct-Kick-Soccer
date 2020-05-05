using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.HelperUtilities
{
    public static class SearchHelper
    {
        public static List<T> SortByMostPopular<T>(List<T> filterableList, List<Order> orderList)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type customProduct = typeof(CustomProduct);

            if (typeof(T) == team)
            {
                // sort by most popular team
                var listAsTeam = filterableList.Cast<Team>().ToList();
                listAsTeam.Sort((team1, team2) => 
                    GetPurchaseCount<Team>(team1.TeamID, orderList).CompareTo(GetPurchaseCount<Team>(team1.TeamID, orderList))
                );
                return listAsTeam.Cast<T>().ToList();
            }
            else if (typeof(T) == customProduct)
            {
                // sort by most popular product
                var listAsProducts = filterableList.Cast<CustomProduct>().ToList();
                listAsProducts.Sort((product1, product2) =>
                    GetPurchaseCount<CustomProduct>(product1.CustomProductID, orderList).CompareTo(GetPurchaseCount<CustomProduct>(product2.CustomProductID, orderList))
                );
                return listAsProducts.Cast<T>().ToList();
            }
            else
            {
                return new List<T>();
            }
        }

        public static BrowseViewModel CreateBrowseObject(
            int currentPageNumber, 
            List<CustomProduct> queriedProducts = null, 
            List<Team> queriedTeams = null, 
            Category categoryObj = null, 
            string searchTerm = null)
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

        public static List<T> FilterByCategory<T>(List<T> filterableList, int categoryId)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type customProduct = typeof(CustomProduct);

            if (typeof(T) == team)
            {
                var listAsTeams = filterableList.Cast<Team>().ToList();
                return listAsTeams.Where(team => team.Category.CategoryID == categoryId).Cast<T>().ToList();
            }
            else if(typeof(T) == customProduct)
            {
                var listAsProducts = filterableList.Cast<CustomProduct>().ToList();
                return listAsProducts.Where(product => product.BaseProduct.Category.CategoryID == categoryId).Cast<T>().ToList();
            }
            else
            {
                return new List<T>();
            }
        }

        public static List<T> SearchByString<T>(List<T> searchableList, string searchString)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type customProduct = typeof(CustomProduct);

            if(typeof(T) == team)
            {
                // begin search on team
                var listAsTeams = searchableList.Cast<Team>().ToList();
                return SearchTeams(listAsTeams, searchString).Cast<T>().ToList();
            }
            else if(typeof(T) == customProduct)
            {
                // beigin search on custom product
                var listAsProducts = searchableList.Cast<CustomProduct>().ToList();
                return SearchCustomProducts(listAsProducts, searchString).Cast<T>().ToList();
            }

            // error occured in type comparisons
            return null;
        }

        // private methods
        private static int GetPurchaseCount<T>(int itemIdArg, List<Order> orderList)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type customProduct = typeof(CustomProduct);

            var purchaseCount = 0;
            foreach (var order in orderList)
            {
                foreach (var itemId in
                    typeof(T) == team ? order.TeamIDs : order.ProductIDs)
                {
                    if (itemId == itemIdArg.ToString())
                        purchaseCount++;
                }
            }

            return purchaseCount;
        }

        private static List<T> SplitList<T>(List<T> filterableList, int start, int end)
        {
            // remember: index is one behind the actual product number in the list
            if (filterableList.Count == 0)
                return filterableList;

            // check if end parameter is higher than remain filterable list count (prevent out of range error
            var checkedEnd = (filterableList.Count - end) < 0 ? filterableList.Count : end;
            var splitList = new List<T>();
            for (var i = start; i <= checkedEnd - 1; i++)
            {
                splitList.Add(filterableList[i]);
            }
            return splitList;
        }

        private static List<object> SearchTeams(List<Team> searchableTeams, string searchString)
        {
            if (searchString.Length >= 2)
            {
                // clean search term
                var cleanedSearchTerm = searchString.Trim().Split(' ');
                var foundTeams = new List<object>();
                // checks team name, tags, category
                foreach (var team in searchableTeams)
                {
                    if (DoesQueryContainString(cleanedSearchTerm, team.Name))
                        foundTeams.Add(team);
                    else if (DoesQueryContainString(cleanedSearchTerm, team.TeamTags))
                        foundTeams.Add(team);
                    else if (DoesQueryContainString(cleanedSearchTerm, team.Category.Name))
                        foundTeams.Add(team);
                    else if (DoesQueryContainString(cleanedSearchTerm, team.Description))
                        foundTeams.Add(team);
                }
                return foundTeams;
            }
            return new List<object>();
        }

        private static List<object> SearchCustomProducts(List<CustomProduct> searchableProducts, string searchString)
        {
            if (searchString.Length >= 2)
            {
                // clean search term
                var cleanedSearchTerm = searchString.Trim().Split(' ');
                var foundProducts = new List<object>();
                // checks product tags, title, color, size, SKU, model number, and category
                foreach (var product in searchableProducts)
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
            return new List<object>();
        }

        private static bool DoesQueryContainString(string[] query, string stringToCheck)
        {
            var stringAsTolken = stringToCheck.Split(' ');
            foreach (var searchTerm in query)
            {
                foreach (var checkAgainstTerm in stringAsTolken)
                {
                    if (searchTerm.ToUpper() == checkAgainstTerm.ToUpper() || checkAgainstTerm.ToUpper().Contains(searchTerm.ToUpper()))
                        return true;
                }
            }
            return false;
        }

        private static bool DoesQueryContainString(string[] query, List<Tag> tagsToCheck)
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
    }
}
