using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        private readonly IActorRepository _actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await _actorRepository.GetActor(id);
            return Ok(actor);
        }

        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            var actors = await _actorRepository.GetActors();
            return Ok(actors);
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(Actor actor)
        {
            await _actorRepository.AddActor(actor);
            return Ok(actor);
        }

    }
}
