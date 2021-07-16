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
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PersonController(IPersonService personService, IMapper mapper, IUriService uriService)
        {
            _personService = personService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await _personService.GetPerson(id);
            var personDto = _mapper.Map<PersonDTO>(person);

            var response = new ApiResponse<PersonDTO>(personDto);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all persons
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPersons))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<GenreDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPersons([FromQuery] GenreQueryFilter filters)
        {
            var persons = _personService.GetPersons(filters);
            var personsDto = _mapper.Map<IEnumerable<PersonDTO>>(persons);

            var metadata = new Metadata
            {
                CurrentPage = persons.CurrentPage,
                PageSize = persons.PageSize,
                TotalPages = persons.TotalPages,
                TotalCount = persons.TotalCount,
                HasNextPage = persons.HasNextPage,
                HasPreviousPage = persons.HasPreviousPage,
                NextPageURL = _uriService.GetGenrePaginationUri(filters, Url.RouteUrl(nameof(GetPersons))).ToString(),
                PreviousPageURL = _uriService.GetGenrePaginationUri(filters, Url.RouteUrl(nameof(GetPersons))).ToString()

            };

            var response = new ApiResponse<IEnumerable<PersonDTO>>(personsDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonDTO personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _personService.AddPerson(person);

            personDto = _mapper.Map<PersonDTO>(person);
            var response = new ApiResponse<PersonDTO>(personDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, PersonDTO personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            person.Id = id;

            var result = await _personService.UpdatePerson(person);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personService.RemovePerson(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
