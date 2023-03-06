using Application.Contracts;
using Domain.Users.ValueObjects;
using ErrorOr;
using MediatR;
using Domain.Common.Errors;

namespace Application.Users.Commands.LogoutCommand;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, ErrorOr<bool>>
{
    private readonly IRefreshTokenRepository _refreshTokens;

    public LogoutCommandHandler(IRefreshTokenRepository refreshTokens)
    {
        _refreshTokens = refreshTokens;
    }

    public async Task<ErrorOr<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.CreateFrom(request.UserId);
        var refreshToken = await _refreshTokens.GetRefreshTokenForUser(userId);

        if (refreshToken is null)
        {
            return Errors.Auth.AlreadyLogedOut;
        }

        return await _refreshTokens.Remove(refreshToken);
    }
}