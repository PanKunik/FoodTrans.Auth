using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.Contracts;

public interface IUserRepository
{
    Task<User> GetUserByEmail(Email email);
    Task<User> GetUserByUsername(Username userName);
    Task<User> AddUser(User user);
}