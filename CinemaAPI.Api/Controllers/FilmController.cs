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
    public class FilmController : ControllerBase
    {

        private readonly IFilmRepository _filmRepository;

        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            var film = await _filmRepository.GetFilm(id);
            return Ok(film);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _filmRepository.GetFilms();
            return Ok(films);
        }

    }
}
