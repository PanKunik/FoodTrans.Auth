using Application.Contracts;
using Domain.Users;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public sealed class UserRepository : IUserRepository
{
    private readonly FoodTransAuthDbContext _dbContext;

    public UserRepository(FoodTransAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> AddUser(User user)
    {
        var result = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public Task<User> GetUserByEmail(Email email)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public Task<User> GetUserByUsername(Username username)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(username));
    }

    public Task<User> GetUserById(UserId userId)
        => _dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
}