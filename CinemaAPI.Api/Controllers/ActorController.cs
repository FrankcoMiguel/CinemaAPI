using CinemaAPI.Core.Entities;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CinemaAPI.Api.Responses;

namespace CinemaAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorController(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var actor = await _actorRepository.GetActor(id);
            var actorDto = _mapper.Map<ActorDTO>(actor);

            var response = new ApiResponse<ActorDTO>(actorDto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var actors = await _actorRepository.GetActors();
            var actorsDto = _mapper.Map<IEnumerable<ActorDTO>>(actors);

            var response = new ApiResponse<IEnumerable<ActorDTO>>(actorsDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActorDTO actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            await _actorRepository.AddActor(actor);

            actorDto = _mapper.Map<ActorDTO>(actor);
            var response = new ApiResponse<ActorDTO>(actorDto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ActorDTO actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            actor.ActorId = id;

            var result = await _actorRepository.UpdateActor(actor);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _actorRepository.RemoveActor(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
