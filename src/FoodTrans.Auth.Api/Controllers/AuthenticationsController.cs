using FoodTrans.Auth.Application.Users.Commands;
using FoodTrans.Auth.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace FoodTrans.Auth.Api.Controllers;

[Route("api/authentications")]
public class AuthenticationsController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(NoContentResult), Status204NoContent)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            user => Ok(user),
            errors => Problem(errors)
        );
    }

    // [HttpPost("login")]
    // public async Task<ActionResult> Login(LoginCommand command)
    // {
    //     return await Task.FromResult(Ok());
    // }

    // [HttpPost("refreshToken")]
    // public async Task<ActionResult> RefreshToken(RefreshTokenCommand command)
    // {
    //     return await Task.FromResult(Ok());
    // }

    // [HttpPost("logout")]
    // public async Task<ActionResult> Logout(LogoutCommand command)
    // {
    //     return await Task.FromResult(Ok());
    // }
}