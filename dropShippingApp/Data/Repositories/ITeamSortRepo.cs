using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamSortRepo
    {
        public List<TeamSort> Sorts { get; }
        public TeamSort GetSortById(int teamId);
        public Task UpdateSort(TeamSort sort);
        public Task AddSort(TeamSort sort);
        public Task<TeamSort> RemoveSort(int sortId);
    }
}
