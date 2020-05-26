using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dropShippingApp.Models;
using dropShippingApp.ViewModels;
using dropShippingApp.Data.Repositories;

namespace dropShippingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APITeamController : ControllerBase
    {
        private ITeamRepo teamRepo;
        public APITeamController(ITeamRepo teamRepo)
        {
            this.teamRepo = teamRepo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceSettings(int id, [FromBody] TeamSettingsViewModel teamSettings)
        {
            if (teamSettings != null)
            {
                Team foundTeam = await teamRepo.FindTeamById(id);
                foundTeam.Name = teamSettings.Name;
                foundTeam.Country = teamSettings.Country;
                foundTeam.Providence = teamSettings.Providence;
                foundTeam.StreetAddress = teamSettings.StreetAddress;
                foundTeam.ZipCode = teamSettings.ZipCode;
                foundTeam.CorporatePageURL = teamSettings.CorporatePageURL;
                foundTeam.BusinessEmail = teamSettings.BusinessEmail;
                foundTeam.PhoneNumber = teamSettings.PhoneNumber;
                foundTeam.Description = teamSettings.Description;
                await teamRepo.UpdateTeam(foundTeam);

                return Ok(foundTeam);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}