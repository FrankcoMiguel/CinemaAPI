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
    [Route("api/[controller]")]
    [ApiController]
    public class CrewController : ControllerBase
    {

        private readonly ICrewRepository _crewRepository;

        public CrewController(ICrewRepository crewRepository)
        {
            _crewRepository = crewRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCrew(int id)
        {
            var crew = await _crewRepository.GetCrew(id);
            return Ok(crew);
        }

        [HttpGet]
        public async Task<IActionResult> GetCrews()
        {
            var crews = await _crewRepository.GetCrews();
            return Ok(crews);
        }

    }
}
