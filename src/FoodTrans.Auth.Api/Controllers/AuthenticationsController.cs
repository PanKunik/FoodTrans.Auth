using Application.Users.Commands.LoginCommand;
using Application.Users.Commands.MeCommand;
using Application.Users.Commands.RegisterCommand;
using Application.Users.Common;
using ErrorOr;
using FoodTrans.Auth.Application.Users.DTO;
using FoodTrans.Auth.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace FoodTrans.Auth.Api.Controllers;

[Route("api/auth")]
public class AuthenticationsController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResult), Status201Created)]
    [ProducesResponseType(typeof(Error), Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            registerResult => Created("api/auth/register", registerResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResult), Status200OK)]
    [ProducesResponseType(typeof(Error), Status400BadRequest)]
    [ProducesResponseType(typeof(Error), Status404NotFound)]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            loginResult => Ok(loginResult),
            errors => Problem(errors)
        );
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(MeDTO), Status200OK)]
    public async Task<IActionResult> Me()
    {
        var command = new MeCommand("patmat12");
        var result = await _mediator.Send(command);

        return result.Match(
            meResult => Ok(meResult),
            errors => Problem(errors)
        );
    }

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