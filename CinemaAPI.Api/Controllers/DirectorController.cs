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
    public class DirectorController : ControllerBase
    {

        private readonly IDirectorRepository _directorRepository;

        public DirectorController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            var director = await _directorRepository.GetDirector(id);
            return Ok(director);
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            var directors = await _directorRepository.GetDirectors();
            return Ok(directors);
        }

    }
}
