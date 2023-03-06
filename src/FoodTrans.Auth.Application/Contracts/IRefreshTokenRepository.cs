using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users.ValueObjects;

namespace Application.Contracts;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    RefreshToken GetRefreshTokenByValue(Token token);
    Task<RefreshToken> GetRefreshTokenForUser(UserId userId);
    Task<RefreshToken> UseToken(RefreshToken refreshToken);
    Task<bool> Remove(RefreshToken token);
}