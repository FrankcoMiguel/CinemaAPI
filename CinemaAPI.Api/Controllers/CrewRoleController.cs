using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Api.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class CrewRoleController : ControllerBase
    {

        private readonly ICrewRoleRepository _crewRoleRepository;

        public CrewRoleController(ICrewRoleRepository crewRoleRepository)
        {
            _crewRoleRepository = crewRoleRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCrewRole(int id)
        {
            var crewRole = await _crewRoleRepository.GetCrewRole(id);
            return Ok(crewRole);
        }

        [HttpGet]
        public async Task<IActionResult> GetCrewRoles()
        {
            var crewRoles = await _crewRoleRepository.GetCrewRoles();
            return Ok(crewRoles);
        }

    }
}