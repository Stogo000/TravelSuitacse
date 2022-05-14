using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelSuitcase.Application.Commands.Auth.LoginUser;
using TravelSuitcase.Application.Commands.Auth.RefreshToken;
using TravelSuitcase.Application.Commands.Auth.RegisterUser;

namespace TravelSuitcase.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand registerUserCommand)
        {
            await _mediator.Send(registerUserCommand);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand loginUserCommand)
        {
            return Ok(await _mediator.Send(loginUserCommand));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            return Ok(await _mediator.Send(refreshTokenCommand));
        }
    }
}