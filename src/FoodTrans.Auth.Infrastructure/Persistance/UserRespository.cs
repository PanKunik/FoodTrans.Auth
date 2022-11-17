using Application.Contracts;
using Domain.User;
using Domain.User.ValueObjects;
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

    public async Task<User> GetUserByEmail(Email email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(userName));
    }
}