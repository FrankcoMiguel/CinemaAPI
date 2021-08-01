using AutoMapper;
using CinemaAPI.Api.Responses;
using CinemaAPI.Core.CustomEntities;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.QueryFilters;
using CinemaAPI.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CinemaAPI.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/film")]
    [ApiController]
    public class FilmController : ControllerBase
    {

        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public FilmController(IFilmService filmService, IMapper mapper, IUriService uriService)
        {
            _filmService = filmService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            var film = await _filmService.GetFilm(id);

            if (film != null)
            {
                var filmDto = _mapper.Map<FilmDTO>(film);

                var response = new ApiResponse<FilmDTO>(filmDto);
                return Ok(response);
            }

            return NotFound();
        }

        /// <summary>
        /// Retrieve all films
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetFilms))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<FilmDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetFilms([FromQuery] FilmQueryFilter filters)
        {
            var films = _filmService.GetFilms(filters);
            var filmsDto = _mapper.Map<IEnumerable<FilmDTO>>(films);

            var metadata = new Metadata
            {
                CurrentPage = films.CurrentPage,
                PageSize = films.PageSize,
                TotalPages = films.TotalPages,
                TotalCount = films.TotalCount,
                HasNextPage = films.HasNextPage,
                HasPreviousPage = films.HasPreviousPage,
                NextPageURL = _uriService.GetFilmPaginationUri(filters, Url.RouteUrl(nameof(GetFilms))).ToString(),
                PreviousPageURL = _uriService.GetFilmPaginationUri(filters, Url.RouteUrl(nameof(GetFilms))).ToString()
            };

            var response = new ApiResponse<IEnumerable<FilmDTO>>(filmsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(FilmDTO filmDto)
        {
            var film = _mapper.Map<Film>(filmDto);
            await _filmService.AddFilm(film);

            filmDto = _mapper.Map<FilmDTO>(film);
            var response = new ApiResponse<FilmDTO>(filmDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, FilmDTO filmDto)
        {
            var film = _mapper.Map<Film>(filmDto);
            film.Id = id;

            var result = await _filmService.UpdateFilm(film);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _filmService.RemoveFilm(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
