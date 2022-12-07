using ErrorOr;
using MediatR;

namespace Application.Users.Commands.RefreshTokenCommand;

public sealed record RefreshTokenCommand(
    Guid RefreshToken) : IRequest<ErrorOr<RefreshTokenResult>>;