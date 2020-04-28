using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ISortRepo
    {
        List<ProductSort> Sorts { get; }
        ProductSort GetSortById(int sortId);
        Task AddSort(ProductSort sort);
        Task<ProductSort> RemoveSortById(int sortId);
    }
}
