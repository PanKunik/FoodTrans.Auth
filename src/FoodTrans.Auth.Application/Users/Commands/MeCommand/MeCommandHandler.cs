using Application.Contracts;
using Domain.Users;
using Domain.Users.ValueObjects;
using ErrorOr;
using MediatR;
using Domain.Common.Errors;

namespace Application.Users.Commands.MeCommand;

public sealed class MeCommandHandler : IRequestHandler<MeCommand, ErrorOr<MeDTO>>
{
    private readonly IUserRepository _userRepository;

    public MeCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<MeDTO>> Handle(MeCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Login);
        var username = Username.Create(request.Login);

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

        return new MeDTO(
            user.FirstName,
            user.LastName,
            user.Email,
            user.Username);
    }
}