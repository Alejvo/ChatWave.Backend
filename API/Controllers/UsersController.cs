﻿using Application.Users.Common;
using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Get.All;
using Application.Users.GetBy.Id;
using Application.Users.GetBy.Username;
using Application.Users.Update;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public UsersController(IMediator mediator, IAuthService authService, IUserRepository userRepository)
        {
            _mediator = mediator;
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById([FromRoute] string id)
        {
            var user = await _mediator.Send(new GetByIdQuery(id));
            return Ok(user);
        }

        [HttpGet]
        [Route("username/{username}")]
        public async Task<ActionResult<UserResponse>> GetUserByUsername([FromRoute] string username)
        {
            var user = await _mediator.Send(new GetByUsernameQuery(username));
            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Updated");

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok("Deleted");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.LoginUser(request.Email,request.Password);
            if(user != null)
            {
                var token = _authService.GenerateToken(user.Id);
                var refreshToken = _authService.GenerateRefreshToken();
                await _authService.SaveRefreshToken("6e283f95-1579-4e92-94c1-0dd386cbea74", refreshToken);
                return Ok(new { token, refreshToken });
            }
            return BadRequest();

        }

    }
}
