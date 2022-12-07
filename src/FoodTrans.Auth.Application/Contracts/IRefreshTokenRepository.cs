using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;

namespace Application.Contracts;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    RefreshToken GetRefreshTokenByValue(Token token);
}