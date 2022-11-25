using Domain.Users;

namespace Application.Contracts;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}