using Application.Contracts;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Domain.Users.ValueObjects;
using Application.Users.Common;
using Domain.Users;

namespace Application.Users.Commands.LoginCommand;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (login, password) = request;

        var username = Username.Create(login);
        var email = Email.Create(login);

        if (username.IsError && email.IsError)
        {
            return Errors.Auth.InvalidCredentials;
        }

        User user;

        if (username.IsError)
        {
            user = await _userRepository.GetUserByEmail(email.Value);
        }
        else
        {
            user = await _userRepository.GetUserByUsername(username.Value);
        }

        if (user is null)
        {
            return Errors.Auth.InvalidCredentials;
        }

        if (!user.Password.Equals(Password.Create(password).IsError ? null : Password.Create(password).Value))
        {
            return Errors.Auth.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user.Email, user.Username, token);
    }
}