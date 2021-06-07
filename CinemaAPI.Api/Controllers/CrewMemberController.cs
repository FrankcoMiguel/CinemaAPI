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
    [Route("api/member")]
    [ApiController]
    public class CrewMemberController : ControllerBase
    {

        private readonly ICrewMemberRepository _crewMemberRepository;

        public CrewMemberController(ICrewMemberRepository crewMemberRepository)
        {
            _crewMemberRepository = crewMemberRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCrewMember(int id)
        {
            var crewMember = await _crewMemberRepository.GetCrewMember(id);
            return Ok(crewMember);
        }

        [HttpGet]
        public async Task<IActionResult> GetCrewMembers()
        {
            var crewMembers = await _crewMemberRepository.GetCrewMembers();
            return Ok(crewMembers);
        }

    }
}