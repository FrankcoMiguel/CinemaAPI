using CinemaAPI.Core.Entities;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

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
            return Ok(actorDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var actors = await _actorRepository.GetActors();
            var actorsDto = _mapper.Map<IEnumerable<ActorDTO>>(actors);
            return Ok(actorsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActorDTO actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            await _actorRepository.AddActor(actor);
            return Ok(actor);
        }

    }
}
