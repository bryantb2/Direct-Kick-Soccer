using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealTeamRequestRepo : ITeamCreationReqRepo
    {
        private ApplicationDbContext context;

        public RealTeamRequestRepo(ApplicationDbContext c)
        {
            this.context = c;
        }
        public IQueryable<TeamCreationRequest>Reqs
        {
            get { return context.TeamCreationRequests.Include("Countries").Include("Providences"); }
        }


        public async Task AddReq(TeamCreationRequest req)
        {
            await context.TeamCreationRequests.AddAsync(req);
            await context.SaveChangesAsync();
        }

        public async Task<List<TeamCreationRequest>> GetAll()
        {
            List<TeamCreationRequest> reqs = (from r in Reqs
                                              select r).ToList();
            return reqs;
        }

        public async Task<TeamCreationRequest> GetById(int id)
        {
            try
            {
                TeamCreationRequest req = (from r in Reqs
                                           where r.TeamCreationRequestID == id
                                           select r).First();
                return req;
            }
            catch
            {
                return null;
            }
        }

      

        public async Task<TeamCreationRequest> MarkAsRejected(int reqId)
        {
            try
            {
                TeamCreationRequest req = (from r in Reqs
                                           where r.TeamCreationRequestID == reqId
                                           select r).First();
                req.IsApproved = false;
                context.Update(req);
                await context.SaveChangesAsync();
                return req;
            }
            catch
            {
                return null;
            }
        }

        public async Task UpdateReq(TeamCreationRequest req)
        {
            context.Update(req);
            await context.SaveChangesAsync();
        }
    }
}
