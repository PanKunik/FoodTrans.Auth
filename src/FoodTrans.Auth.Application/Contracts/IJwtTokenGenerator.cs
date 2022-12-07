using Domain;
using Domain.Users;

namespace Application.Contracts;

public interface IJwtTokenGenerator
{
    JwtToken GenerateToken(User user);
}