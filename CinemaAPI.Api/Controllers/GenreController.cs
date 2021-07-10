using AutoMapper;
using CinemaAPI.Api.Responses;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CinemaAPI.Api.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await _genreService.GetGenre(id);
            var genreDto = _mapper.Map<GenreDTO>(genre);

            var response = new ApiResponse<GenreDTO>(genreDto);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll([FromQuery] GenreQueryFilter filters)
        {
            var genres = _genreService.GetGenres(filters);
            var genresDto = _mapper.Map<IEnumerable<GenreDTO>>(genres);
            var response = new ApiResponse<IEnumerable<GenreDTO>>(genresDto);

            var metadata = new
            {
                genres.TotalCount,
                genres.PageSize,
                genres.CurrentPage,
                genres.TotalPages,
                genres.HasNextPage,
                genres.HasPreviousPage
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
