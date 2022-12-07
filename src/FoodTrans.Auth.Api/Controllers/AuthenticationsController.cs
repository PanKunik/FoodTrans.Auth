using Application.Users.Commands.LoginCommand;
using Application.Users.Commands.MeCommand;
using Application.Users.Commands.RegisterCommand;
using Application.Users.Commands.RefreshTokenCommand;
using ErrorOr;
using FoodTrans.Auth.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Application.Users.Commands.LogoutCommand;
using Api.Services;

namespace FoodTrans.Auth.Api.Controllers;

[Route("api/auth")]
public class AuthenticationsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public AuthenticationsController(IMediator mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResult), Status201Created)]
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
    [ProducesResponseType(typeof(LoginResult), Status200OK)]
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
        var command = new MeCommand(_userService.GetUsername());
        var result = await _mediator.Send(command);

        return result.Match(
            meResult => Ok(meResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("refreshToken")]
    [ProducesResponseType(typeof(RefreshTokenResult), Status200OK)]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            refreshTokenResult => Ok(refreshTokenResult),
            errors => Problem(errors)
        );
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var command = new LogoutCommand(_userService.GetUserId());
        var result = await _mediator.Send(command);

        return result.Match(
            isSuccessfull => Ok(isSuccessfull),
            errors => Problem(errors)
        );
    }
}