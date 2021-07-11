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
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GenreController(IGenreService genreService, IMapper mapper, IUriService uriService)
        {
            _genreService = genreService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _genreService.GetGenre(id);
            var genreDto = _mapper.Map<GenreDTO>(genre);

            var response = new ApiResponse<GenreDTO>(genreDto);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all genres
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetGenres))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<GenreDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetGenres([FromQuery] GenreQueryFilter filters)
        {
            var genres = _genreService.GetGenres(filters);
            var genresDto = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            var metadata = new Metadata
            {
                CurrentPage = genres.CurrentPage,
                PageSize = genres.PageSize,
                TotalPages = genres.TotalPages,
                TotalCount = genres.TotalCount,
                HasNextPage = genres.HasNextPage,
                HasPreviousPage = genres.HasPreviousPage,
                NextPageURL = _uriService.GetGenrePaginationUri(filters, Url.RouteUrl(nameof(GetGenres))).ToString(),
                PreviousPageURL = _uriService.GetGenrePaginationUri(filters, Url.RouteUrl(nameof(GetGenres))).ToString()

            };

            var response = new ApiResponse<IEnumerable<GenreDTO>>(genresDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreDTO genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            await _genreService.AddGenre(genre);

            genreDto = _mapper.Map<GenreDTO>(genre);
            var response = new ApiResponse<GenreDTO>(genreDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, GenreDTO genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            genre.Id = id;

            var result = await _genreService.UpdateGenre(genre);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _genreService.RemoveGenre(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
