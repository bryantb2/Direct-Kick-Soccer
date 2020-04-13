using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealUserRepo : IUserRepo
    {
        private ApplicationDbContext context;
        private UserManager<AppUser> userManager;
        public RealUserRepo(UserManager<AppUser> usrMgr, ApplicationDbContext c)
        {
            this.context = c;
            this.userManager = usrMgr;
        }

        public List<AppUser> GetAllUsersAndData()
        {
            var userList = userManager.Users
                .Include(usr => usr.GetMessageList)
                .ThenInclude(msg => msg.GetReplyHistory)
            .Include(usr => usr.GetReplyHistory)
                .ToList();
            return userList;
        }

        public async Task<AppUser> GetUserDataAsync(ClaimsPrincipal User)
        {
            // get user async to extract id
            var user = await userManager.GetUserAsync(User);
            return this.context.Users
                .Include(usr => usr.GetMessageList)
                    .ThenInclude(msg => msg.GetReplyHistory)
                .Include(usr => usr.GetReplyHistory)
                    .ToList().Find(usr => usr.Id == user.Id);
        }
    }
}
