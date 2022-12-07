using Application.Contracts;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Domain.Users.ValueObjects;
using Domain.Users;
using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;

namespace Application.Users.Commands.LoginCommand;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
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

        if (await _refreshTokenRepository.GetRefreshTokenForUser(user.Id) != null)
        {
            return Errors.Auth.AlreadyLogedIn;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var refreshToken = RefreshToken.Create(
            Token.Create(token.RefreshToken),
            ExpiresAt.Create(token.ExpiresAt.AddDays(7)).Value,
            user.Id);

        await _refreshTokenRepository.AddRefreshToken(refreshToken.Value);

        return new LoginResult(user.Email, user.Username, token.Value, token.ExpiresAt, token.RefreshToken);
    }
}