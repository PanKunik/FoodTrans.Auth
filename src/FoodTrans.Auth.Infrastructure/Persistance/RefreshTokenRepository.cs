using Application.Contracts;
using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;

namespace Infrastructure.Persistance;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly FoodTransAuthDbContext _dbContext;

    public RefreshTokenRepository(FoodTransAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        var result = await _dbContext.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public RefreshToken GetRefreshTokenByValue(Token token)
    {
        return _dbContext.RefreshTokens.First(x => x.Token.Equals(token));
    }
}