﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Models;

namespace dropShippingApp.Data.Repositories
{
    public interface ITeamCreationReqRepo
    {
        Task AddReq(TeamCreationRequest req);
        Task<TeamCreationRequest> MarkAsRejected(int reqId);
        Task UpdateReq(TeamCreationRequest req);
        Task<List<TeamCreationRequest>> GetAll();
        Task<TeamCreationRequest> GetById(int id);
    }
}
