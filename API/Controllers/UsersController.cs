using Application.Users.Common;
using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Friends;
using Application.Users.Get.All;
using Application.Users.GetBy.Id;
using Application.Users.GetBy.Username;
using Application.Users.Update;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Controllers
{
    [Route("api/users")]
    //[Authorize]
    public class UsersController : ApiController
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return users.Match(
                result=>Ok(result),
                errors=>Problem(errors));
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _mediator.Send(new GetByIdQuery(id));
            return user.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("username/{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            var user = await _mediator.Send(new GetByUsernameQuery(username));
            return user.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var updatedUser = await _mediator.Send(command);
            return updatedUser.Match(
                result=>NoContent(),
                errors=>Problem(errors));

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var deletedUser = await _mediator.Send(new DeleteUserCommand(id));
            return deletedUser.Match(
                result=>NoContent(),
                errors=>Problem(errors));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.LoginUser(request.Email,request.Password);
            if(user != null)
            {
                var token = _authService.GenerateToken(user.Id,user.UserName);
                var refreshToken = _authService.GenerateRefreshToken();
                await _authService.SaveRefreshToken(user.Id, refreshToken);
                return Ok(new { token, refreshToken });
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var createdUser = await _mediator.Send(command);
            return createdUser.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("add-friend")]
        public async Task<IActionResult> AddFriend([FromBody] AddFriendCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match((res) => NoContent(), (errors) => Problem(errors));
        }

    }
}
