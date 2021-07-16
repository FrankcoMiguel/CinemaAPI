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
    [Route("api/occupation")]
    [ApiController]
    public class OccupationController : ControllerBase
    {

        private readonly IOccupationService _occupationService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public OccupationController(IOccupationService occupationService, IMapper mapper, IUriService uriService)
        {
            _occupationService = occupationService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOccupation(int id)
        {
            var occupation = await _occupationService.GetOccupation(id);
            var occupationDto = _mapper.Map<OccupationDTO>(occupation);

            var response = new ApiResponse<OccupationDTO>(occupationDto);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all genres
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetOccupations))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<OccupationDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetOccupations([FromQuery] FilmQueryFilter filters)
        {
            var occupations = _occupationService.GetOccupations(filters);
            var occupationsDto = _mapper.Map<IEnumerable<OccupationDTO>>(occupations);

            var metadata = new Metadata
            {
                CurrentPage = occupations.CurrentPage,
                PageSize = occupations.PageSize,
                TotalPages = occupations.TotalPages,
                TotalCount = occupations.TotalCount,
                HasNextPage = occupations.HasNextPage,
                HasPreviousPage = occupations.HasPreviousPage,
                NextPageURL = _uriService.GetFilmPaginationUri(filters, Url.RouteUrl(nameof(GetOccupations))).ToString(),
                PreviousPageURL = _uriService.GetFilmPaginationUri(filters, Url.RouteUrl(nameof(GetOccupations))).ToString()

            };

            var response = new ApiResponse<IEnumerable<OccupationDTO>>(occupationsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OccupationDTO occupationDto)
        {
            var occupation = _mapper.Map<Occupation>(occupationDto);
            await _occupationService.AddOccupation(occupation);

            occupationDto = _mapper.Map<OccupationDTO>(occupation);
            var response = new ApiResponse<OccupationDTO>(occupationDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, OccupationDTO occupationDto)
        {
            var occupation = _mapper.Map<Occupation>(occupationDto);
            occupation.Id = id;

            var result = await _occupationService.UpdateOccupation(occupation);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _occupationService.RemoveOccupation(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
