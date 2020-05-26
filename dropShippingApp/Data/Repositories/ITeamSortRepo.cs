using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamSortRepo
    {
        List<TeamSort> Sorts { get; }
        TeamSort GetSortById(int teamId);
        Task UpdateSort(TeamSort sort);
        Task AddSort(TeamSort sort);
        Task<TeamSort> RemoveSort(int sortId);
    }
}
