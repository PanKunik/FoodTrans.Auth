using Application.Users.Commands.LoginCommand;
using Application.Users.Commands.RegisterCommand;
using Domain.Users;
using ErrorOr;
using FoodTrans.Auth.Application.Users.DTO;
using FoodTrans.Auth.Controllers.Common;
using MediatR;
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
    [ProducesResponseType(typeof(UserDTO), Status201Created)]
    [ProducesResponseType(typeof(Error), Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            user => Created("api/auth/register", MapToDTO(user)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(UserDTO), Status200OK)]
    [ProducesResponseType(typeof(Error), Status400BadRequest)]
    [ProducesResponseType(typeof(Error), Status404NotFound)]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            user => Ok(MapToDTO(user)),
            errors => Problem(errors)
        );
    }

    // TODO: Add mapping NuGet package (Mapper or Mapster)
    private static UserDTO MapToDTO(User user)
        => new()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username
        };

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