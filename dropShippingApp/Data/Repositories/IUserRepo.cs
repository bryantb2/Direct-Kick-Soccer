using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IUserRepo
    {
        Task<AppUser> GetUserDataAsync(ClaimsPrincipal User);
        List<AppUser> GetAllUsersAndData();
    }
}
