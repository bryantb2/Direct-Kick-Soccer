using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamSortRepo
    {
        List<Sort> Sorts { get; }
        Sort GetSortById(int teamId);
        Task UpdateSort(Sort sort);
        Task AddSort(Sort sort);
        Task<Sort> RemoveSort(int sortId);
    }
}
