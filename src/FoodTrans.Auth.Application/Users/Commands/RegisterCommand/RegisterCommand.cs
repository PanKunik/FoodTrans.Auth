using Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.RegisterCommand;

public sealed record RegisterCommand(
    string Email,
    string Username,
    string FirstName,
    string LastName,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;