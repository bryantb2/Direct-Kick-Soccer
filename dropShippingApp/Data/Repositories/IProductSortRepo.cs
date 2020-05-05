using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IProductSortRepo
    {
        List<Sort> Sorts { get; }
        Sort GetSortById(int sortId);
        Task AddSort(Sort sort);
        Task<Sort> RemoveSortById(int sortId);
    }
}
