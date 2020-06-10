using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.FakeRepos
{
    public class FakeCreationReqRepo : ITeamCreationReqRepo
    {
        private List<TeamCreationRequest> reqs = new List<TeamCreationRequest>();
        public List<TeamCreationRequest>GetReqs
        {
            get
            {
                return this.reqs;
            }
        }
        public async Task AddReq(TeamCreationRequest req)
        {
            reqs.Add(req);
        }

        public Task<List<TeamCreationRequest>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamCreationRequest>> GetApproved()
        {
            throw new NotImplementedException();
        }

        //public async Task<List<TeamCreationRequest>> GetAll()
        //{
        //    List<TeamCreationRequest> teamReq = (from r in reqs
        //                                      select r).ToList();
        //    return teamReq;
        //}

        public async Task<TeamCreationRequest> GetById(int id)
        {
            TeamCreationRequest req = (from r in reqs
                                       where r.TeamCreationRequestID == id
                                       select r).FirstOrDefault();
            if(req!=null)
            {
                return await Task.FromResult<TeamCreationRequest>(req);
            }
            else
            {
                return await Task.FromResult<TeamCreationRequest>(null);
            }
        }

        public Task<List<TeamCreationRequest>> GetDenied()
        {
            throw new NotImplementedException();
        }

        public Task<TeamCreationRequest> MarkAsApproved(int reqId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamCreationRequest> MarkAsRejected(int reqId)
        {
            TeamCreationRequest req = (from r in reqs
                                       where r.TeamCreationRequestID == reqId
                                       select r).FirstOrDefault();
            req.IsApproved = false;

            return Task.FromResult<TeamCreationRequest>(req);
        }

        public async Task UpdateReq(TeamCreationRequest req)
        {
            TeamCreationRequest result = reqs.Find(r => r.TeamCreationRequestID == r.TeamCreationRequestID);
            reqs.Remove(result);
            reqs.Add(req);
        }

        Task<List<TeamCreationRequest>> ITeamCreationReqRepo.GetReqsToCheck()
        {
            throw new NotImplementedException();
        }
    }
}
