using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace dropShippingApp.Controllers
{
    public class AdminController : Controller
    {
        private ITeamCreationReqRepo requestRepo;
        public AdminController(ITeamCreationReqRepo c)
        {
            requestRepo = c;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewRequestAsync(int id)
        {
            TeamCreationRequest req = await requestRepo.GetById(id);
            return View(req);                    
        }
        [HttpPost]
        public IActionResult ViewRequest(int id,string command)
        {
            if(command=="approve")
            {
                requestRepo.MarkAsApproved(id);
                return View("ApproveTeamRequests");
            }
            else
            {
                requestRepo.MarkAsRejected(id);
                return View("ApproveTeamRequests");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ApproveTeamRequestsAsync()
        {
            List<TeamCreationRequest> reqs = await requestRepo.GetReqsToCheck();
            return View(reqs);
                                           
        }
        [HttpGet]
        public async Task<IActionResult> ViewApprovedRequests()
        {
            List<TeamCreationRequest> reqs = await requestRepo.GetApproved();
            return View("Index", reqs);
        }
        [HttpGet]
        public async Task<IActionResult> ViewDeniedRequests()
        {
            List<TeamCreationRequest> reqs = await requestRepo.GetDenied();
            return View("Index", reqs);
        }


    }
}