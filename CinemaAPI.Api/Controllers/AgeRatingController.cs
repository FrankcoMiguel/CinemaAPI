using AutoMapper;
using CinemaAPI.Api.Responses;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CinemaAPI.Api.Controllers
{
    [Route("api/age-rating")]
    [ApiController]
    public class AgeRatingController : ControllerBase
    {

        private readonly IAgeRatingService _ageRatingService;
        private readonly IMapper _mapper;

        public AgeRatingController(IAgeRatingService ageRatingService, IMapper mapper)
        {
            _ageRatingService = ageRatingService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ageRating = await _ageRatingService.GetAgeRating(id);
            var ageRatingDto = _mapper.Map<AgeRatingDTO>(ageRating);

            var response = new ApiResponse<AgeRatingDTO>(ageRatingDto);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll([FromQuery] AgeRatingQueryFilter filters)
        {
            var ageRatings = _ageRatingService.GetAgeRatings(filters);
            var ageRatingsDto = _mapper.Map<IEnumerable<AgeRatingDTO>>(ageRatings);

            var response = new ApiResponse<IEnumerable<AgeRatingDTO>>(ageRatingsDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AgeRatingDTO ageRatingDto)
        {
            var ageRating = _mapper.Map<AgeRating>(ageRatingDto);
            await _ageRatingService.AddAgeRating(ageRating);

            ageRatingDto = _mapper.Map<AgeRatingDTO>(ageRating);
            var response = new ApiResponse<AgeRatingDTO>(ageRatingDto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, AgeRatingDTO ageRatingDto)
        {
            var ageRating = _mapper.Map<AgeRating>(ageRatingDto);
            ageRating.Id = id;

            var result = await _ageRatingService.UpdateAgeRating(ageRating);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ageRatingService.RemoveAgeRating(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
