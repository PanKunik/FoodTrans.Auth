using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.LoginCommand;

public sealed record LoginCommand(
    string Login,
    string Password) : IRequest<ErrorOr<User>>;