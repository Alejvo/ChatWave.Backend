using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public TokensController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Token model)
        {
            var token = _authService.GenerateToken(model.UserId);
            var refreshToken = _authService.GenerateRefreshToken();
            await _authService.SaveRefreshToken("6e283f95-1579-4e92-94c1-0dd386cbea74",refreshToken);
            return Ok(new {token,refreshToken});
        }

    }
}
