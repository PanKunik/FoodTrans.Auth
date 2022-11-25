using Application.Contracts;
using Domain.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Domain.Users.ValueObjects;

namespace Application.Users.Commands.LoginCommand;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (login, password) = request;

        var user = await _userRepository.GetUserByUsername(Username.Create(login).IsError ? null : Username.Create(login).Value)
                   ?? await _userRepository.GetUserByEmail(Email.Create(login).IsError ? null : Email.Create(login).Value);

        if (user is null)
        {
            return Errors.Auth.InvalidCredentials;
        }

        if (!user.Password.Equals(Password.Create(password).IsError ? null : Password.Create(password).Value))
        {
            return Errors.Auth.InvalidCredentials;
        }

        return user;
    }
}