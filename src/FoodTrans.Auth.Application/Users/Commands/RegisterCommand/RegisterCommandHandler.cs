using Application.Contracts;
using Domain.Users;
using Domain.Users.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.RegisterCommand;


public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRespository;

    public RegisterCommandHandler(IUserRepository userRespository)
    {
        _userRespository = userRespository;
    }

    public async Task<ErrorOr<User>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.Errors;
        }

        var username = Username.Create(request.Username);

        if (username.IsError)
        {
            return username.Errors;
        }

        var firstName = FirstName.Create(request.FirstName);

        if (firstName.IsError)
        {
            return firstName.Errors;
        }

        var lastName = LastName.Create(request.LastName);

        if (lastName.IsError)
        {
            return lastName.Errors;
        }

        var password = Password.Create(request.Password);

        if (password.IsError)
        {
            return password.Errors;
        }

        // TODO: Wielokrotne sprawdzanie czy wystąpił błąd, zbyt długi kod

        var user = User.Create(
            email.Value,
            username.Value,
            firstName.Value,
            lastName.Value,
            password.Value);

        if (!user.IsError)
            await _userRespository.AddUser(user.Value);

        return user;
    }
}