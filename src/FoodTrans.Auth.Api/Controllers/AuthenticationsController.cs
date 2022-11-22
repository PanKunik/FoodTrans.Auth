using Application.Users.Commands;
using Domain.Users;
using ErrorOr;
using FoodTrans.Auth.Application.Users.Commands;
using FoodTrans.Auth.Application.Users.DTO;
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
    [ProducesResponseType(typeof(User), Status201Created)]
    [ProducesResponseType(typeof(Error), Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            user => Created("api/authentications/register", MapToDTO(user)),
            errors => Problem(errors)
        );
    }

    private UserDTO MapToDTO(User user)
        => new()
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            FirstName = user.FirstName.Value,
            LastName = user.LastName.Value,
            UserName = user.Username.Value
        };

    [HttpPost("login")]
    [ProducesResponseType(typeof(User), Status200OK)]
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