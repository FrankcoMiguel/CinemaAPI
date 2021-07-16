using AutoMapper;
using CinemaAPI.Api.Responses;
using CinemaAPI.Core.DTOs;
using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Enumerations;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaAPI.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Admin))]
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserController(IUserService userService, IMapper mapper, IPasswordService passwordService)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = _passwordService.Hash(user.Password);
            await _userService.RegisterUser(user);
            userDto = _mapper.Map<UserDTO>(user);
            var response = new ApiResponse<UserDTO>(userDto);
            return Ok(response);
        }

    }
}
