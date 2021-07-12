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
    [Route("api/rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {

        private readonly IRatingService _ratingService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public RatingController(IRatingService ratingService, IMapper mapper, IUriService uriService)
        {
            _ratingService = ratingService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var rating = await _ratingService.GetRating(id);
            var ratingDto = _mapper.Map<RatingDTO>(rating);

            var response = new ApiResponse<RatingDTO>(ratingDto);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all ratings
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRatings))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<GenreDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRatings([FromQuery] GenreQueryFilter filters)
        {
            var ratings = _ratingService.GetRatings(filters);
            var ratingsDto = _mapper.Map<IEnumerable<RatingDTO>>(ratings);

            var metadata = new Metadata
            {
                CurrentPage = ratings.CurrentPage,
                PageSize = ratings.PageSize,
                TotalPages = ratings.TotalPages,
                TotalCount = ratings.TotalCount,
                HasNextPage = ratings.HasNextPage,
                HasPreviousPage = ratings.HasPreviousPage,
                NextPageURL = _uriService.GetGenrePaginationUri(filters, Url.RouteUrl(nameof(GetRatings))).ToString(),
                PreviousPageURL = _uriService.GetGenrePaginationUri(filters, Url.RouteUrl(nameof(GetRatings))).ToString()

            };

            var response = new ApiResponse<IEnumerable<RatingDTO>>(ratingsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RatingDTO ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            await _ratingService.AddRating(rating);

            ratingDto = _mapper.Map<RatingDTO>(rating);
            var response = new ApiResponse<RatingDTO>(ratingDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, RatingDTO ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            rating.Id = id;

            var result = await _ratingService.UpdateRating(rating);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ratingService.RemoveRating(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
