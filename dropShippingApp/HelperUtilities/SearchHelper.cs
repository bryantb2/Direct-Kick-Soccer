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
        public static void SortGroupsByPrice(ref List<ProductGroup> filterableList, int sortBy = 1)
        {
            // sortby parameter is 1 by default. Sort by highest price is 1 and lowest is -1
            if(sortBy != -1 || sortBy != 1)
            {
                // sort groups
                if (sortBy == -1)
                {
                    // sort by lowest price
                    filterableList.Sort((group1, group2) => 
                        group1.GetHighestPrice.CompareTo(group2.GetHighestPrice));
                }
                else
                {
                    // sort by highest price
                    filterableList.Sort((group1, group2) =>
                        group2.GetHighestPrice.CompareTo(group1.GetHighestPrice));
                }
            }
            //return filterableList;
        }

        public static void SortByMostPopular<T>(ref List<T> filterableList, List<Order> orderList)
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
                listAsTeam.Cast<T>().ToList();
            }
            else if (typeof(T) == customProduct)
            {
                // TODO: FIX THIS TO USE PRODUCT GROUP INSTEAD

                // sort by most popular product
                var listAsProducts = filterableList.Cast<CustomProduct>().ToList();
                listAsProducts.Sort((product1, product2) =>
                    GetPurchaseCount<CustomProduct>(product1.CustomProductID, orderList).CompareTo(GetPurchaseCount<CustomProduct>(product2.CustomProductID, orderList))
                );
                listAsProducts.Cast<T>().ToList();
            }
        }

        public static BrowseViewModel CreateBrowseObject<T>(
            int currentPageNumber, 
            List<ProductGroup> queriedGroups = null, 
            List<Team> queriedTeams = null, 
            TeamCategory teamCategory = null, 
            ProductCategory productCategory = null,
            string searchTerm = null)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type productGroup = typeof(ProductGroup);

            // setup starting and ending product indexes
            var itemsPerPage = 30;
            var startProduct = currentPageNumber * itemsPerPage;
            var endProduct = startProduct + 30;

            // setup paging view model
            var pagingInfo = new BrowseViewModel()
            {
                CurrentPage = currentPageNumber,
                SearchString = searchTerm == null ? null : searchTerm,
                // next page exists if the number of products left in the query is greater than the total number of dispalyed products
                PreviousPageExists = startProduct - itemsPerPage > 0 ? true : false
            };
            if (team == typeof(T))
            {
                pagingInfo.Teams = SplitList(queriedTeams, startProduct, endProduct);
                pagingInfo.NextPageExists = queriedTeams.Count > endProduct ? true : false;
                pagingInfo.CurrentTeamCategory = teamCategory;
            }
            else if(productGroup == typeof(T))
            {
                pagingInfo.ProductGroups = SplitList(queriedGroups, startProduct, endProduct);
                pagingInfo.NextPageExists = queriedGroups.Count > endProduct ? true : false;
                pagingInfo.CurrentProductCategory = productCategory;
            }
            return pagingInfo;
        }

        public static List<T> FilterByCategory<T>(List<T> filterableList, int categoryId)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type productGroup = typeof(ProductGroup);

            if (typeof(T) == team)
            {
                var listAsTeams = filterableList.Cast<Team>().ToList();
                return listAsTeams.Where(team => team.Category.TeamCategoryID == categoryId).Cast<T>().ToList();
            }
            else if(typeof(T) == productGroup)
            {
                var listAsGroups = filterableList.Cast<ProductGroup>().ToList();
                var filteredGroups = new List<ProductGroup>(listAsGroups.ToArray());
                foreach (var group in listAsGroups)
                {
                    // remove current item if it's product does not contain the current category
                    var filteredGroupProducts = group.ChildProducts.Where(product => product.BaseProduct.RosterGroup.Category.ProductCategoryID == categoryId);
                    if (filteredGroupProducts.Count() == 0)
                        filteredGroups.Remove(group);
                }
                return filteredGroups.Cast<T>().ToList();
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
            Type productGroup = typeof(ProductGroup);

            if(typeof(T) == team)
            {
                // begin search on team
                var listAsTeams = searchableList.Cast<Team>().ToList();
                return SearchTeams(listAsTeams, searchString).Cast<T>().ToList();
            }
            else if(typeof(T) == productGroup)
            {
                // begin search on product family
                var listAsProductGroups = searchableList.Cast<ProductGroup>().ToList();
                return SearchProductGroups(listAsProductGroups, searchString).Cast<T>().ToList();
            }

            // error occured in type comparisons
            return null;
        }

        // private methods
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

        private static List<object> SearchProductGroups(List<ProductGroup> searchableGroups, string searchString)
        {
            try
            {
                var foundGroups = new List<object>();
                if (searchString.Length >= 2)
                {
                    var cleanedSearchTerm = searchString.Trim().Split(' ');
                    var hasMatch = false;
                    foreach (var group in searchableGroups)
                    {
                        // clean search term
                        // checks product tags, title, color, size, SKU, model number, and category
                        if (DoesQueryContainString(cleanedSearchTerm, group.Description))
                            hasMatch = true;
                        else if (DoesQueryContainString(cleanedSearchTerm, group.Title))
                            hasMatch = true;
                        else if (DoesQueryContainString(cleanedSearchTerm, group.ProductTags))
                            hasMatch = true;
                        if (!hasMatch)
                        {
                            foreach (var product in group.ChildProducts)
                            {
                                if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.RosterGroup.ProductTags))
                                    hasMatch = true;
                                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.RosterGroup.ModelNumber.ToString()))
                                    hasMatch = true;
                                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.SKU.ToString()))
                                    hasMatch = true;
                                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.BaseColor.ColorName))
                                    hasMatch = true;
                                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.BaseSize.SizeName))
                                    hasMatch = true;
                                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.RosterGroup.ProductTags))
                                    hasMatch = true;
                                else if (DoesQueryContainString(cleanedSearchTerm, product.BaseProduct.RosterGroup.Category.Name))
                                    hasMatch = true;
                                // no need to keep searching if we already have one match
                                if (hasMatch)
                                    break;
                            }
                        }
                        if (hasMatch)
                        {
                            foundGroups.Add(group);
                            hasMatch = false;
                        }
                    }
                    return foundGroups;
                }
                return foundGroups;
            }
            catch
            {
                return null;
            }
           
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
        private static int GetPurchaseCount<T>(int itemIdArg, List<Order> orderList)
        {
            // setup for type comparisons
            Type team = typeof(Team);
            Type productGroup = typeof(ProductGroup);

            var purchaseCount = 0;
            // loop through each order
            foreach (var order in orderList)
            {
                // loop through each item in order
                foreach (var item in order.OrderedItems)
                {
                    // check type to determine comparison behaviors
                    if (typeof(T) == team)
                    {
                        if (item.TeamID == itemIdArg.ToString())
                            purchaseCount++;
                    }
                    else if(typeof(T) == productGroup)
                    {
                        if (item.ProductID == itemIdArg.ToString())
                            purchaseCount++;
                    }
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

    }
}
