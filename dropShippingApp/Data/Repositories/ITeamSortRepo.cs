using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ISortRepo
    {
        public List<Sort> Sorts { get; }
        public Sort GetSortById(int teamId);
        public Task UpdateSort(Sort sort);
        public Task AddSort(Sort sort);
        public Task<Sort> RemoveSort(int sortId);
    }
}
