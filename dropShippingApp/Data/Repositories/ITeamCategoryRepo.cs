using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamCategoryRepo
    {
        List<TeamCategory> GetCategories { get; }
        TeamCategory GetCategoryById(int categoryId);
        Task UpdateCategory(TeamCategory category);
        Task AddCategory(TeamCategory category);
        Task<TeamCategory> RemoveCategory(int categoryId);
    }
}
