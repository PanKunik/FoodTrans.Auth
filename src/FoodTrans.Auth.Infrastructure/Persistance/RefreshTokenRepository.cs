using Application.Contracts;
using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

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

    public async Task<RefreshToken> UseToken(RefreshToken refreshToken)
    {
        var result = _dbContext.RefreshTokens.Update(refreshToken);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public RefreshToken GetRefreshTokenByValue(Token token)
    {
        return _dbContext.RefreshTokens.First(x => x.Token.Equals(token) && x.ExpiresAt > DateTime.UtcNow);
    }

    public Task<RefreshToken> GetRefreshTokenForUser(UserId userId)
    {
        return _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ExpiresAt > DateTime.UtcNow && !x.WasUsed);
    }

    public async Task<bool> Remove(RefreshToken token)
    {
        var result = _dbContext.RefreshTokens.Remove(token);
        var isSuccessfull = result.State.Equals(EntityState.Deleted);
        await _dbContext.SaveChangesAsync();
        return isSuccessfull;
    }
}