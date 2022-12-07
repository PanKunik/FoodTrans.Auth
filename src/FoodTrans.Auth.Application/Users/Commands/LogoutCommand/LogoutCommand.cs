using ErrorOr;
using MediatR;

namespace Application.Users.Commands.LogoutCommand;

public sealed record LogoutCommand(
    Guid UserId) : IRequest<ErrorOr<bool>>;