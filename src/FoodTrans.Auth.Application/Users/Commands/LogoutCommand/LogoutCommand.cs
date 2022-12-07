using ErrorOr;
using MediatR;

namespace Application.Users.Commands.LogoutCommand;

public sealed record LogoutCommand(
    string RefreshToken) : IRequest<ErrorOr<bool>>;