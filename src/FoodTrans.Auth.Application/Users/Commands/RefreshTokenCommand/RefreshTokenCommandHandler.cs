using Application.Contracts;
using Domain.Common.Errors;
using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.RefreshTokenCommand;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RefreshTokenCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenValue = Token.Create(request.RefreshToken);
        var refreshToken = _refreshTokenRepository.GetRefreshTokenByValue(refreshTokenValue);

        if (refreshToken is null || refreshToken.WasUsed)
        {
            return Errors.RefreshToken.TokenHasExpired;
        }

        var user = await _userRepository.GetUserById(refreshToken.UserId);

        if (user is null)
        {
            return Errors.Auth.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        refreshToken.Use();
        await _refreshTokenRepository.UseToken(refreshToken);

        var newRefreshToken = RefreshToken.Create(
            Token.Create(token.RefreshToken),
            ExpiresAt.Create(token.ExpiresAt.AddDays(7)).Value,
            user.Id);

        await _refreshTokenRepository.AddRefreshToken(newRefreshToken.Value);

        return new RefreshTokenResult(token.Value, token.RefreshToken, token.ExpiresAt);
    }
}