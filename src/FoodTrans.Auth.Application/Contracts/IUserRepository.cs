using Domain.User;
using Domain.User.ValueObjects;

namespace Application.Contracts;

public interface IUserRepository
{
    Task<User> GetUserByEmail(Email email);
    Task<User> GetUserByUserName(string userName);
    Task<User> AddUser(User user);
}