using CinemaAPI.Core.Entities;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CinemaAPI.Api.Responses;
using CinemaAPI.Core.QueryFilters;
using System.Net;

namespace CinemaAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        public ActorController(IActorService actorService, IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var actor = await _actorService.GetActor(id);
            var actorDto = _mapper.Map<ActorDTO>(actor);

            var response = new ApiResponse<ActorDTO>(actorDto);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll([FromQuery]ActorQueryFilter filters)
        {
            var actors = _actorService.GetActors(filters);
            var actorsDto = _mapper.Map<IEnumerable<ActorDTO>>(actors);

            var response = new ApiResponse<IEnumerable<ActorDTO>>(actorsDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActorDTO actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            await _actorService.AddActor(actor);

            actorDto = _mapper.Map<ActorDTO>(actor);
            var response = new ApiResponse<ActorDTO>(actorDto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ActorDTO actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            actor.Id = id;

            var result = await _actorService.UpdateActor(actor);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _actorService.RemoveActor(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
