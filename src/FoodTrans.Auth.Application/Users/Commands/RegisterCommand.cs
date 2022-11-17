using Application.Contracts;
using Domain.User;
using Domain.User.ValueObjects;
using ErrorOr;
using MediatR;

namespace FoodTrans.Auth.Application.Users.Commands;

public sealed class RegisterCommand : IRequest<ErrorOr<User>>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}

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
            return Error.Conflict();    // TODO: Zwrócenie niepoprawnego błędu - powiniene dotyczyć adresu email
        }

        var username = Username.Create(request.Username);

        if (username.IsError)
        {
            return Error.Conflict();    // TODO: Zwrócenie niepoprawnego błędu - powiniene dotyczyć nazwy użytkownika
        }

        var firstName = FirstName.Create(request.FirstName);

        if (firstName.IsError)
        {
            return Error.Conflict();    // TODO: Zwrócenie niepoprawnego błędu - powiniene dotyczyć imienia
        }

        var lastName = LastName.Create(request.LastName);

        if (lastName.IsError)
        {
            return Error.Conflict();    // TODO: Zwrócenie niepoprawnego błędu - powiniene dotyczyć nazwiska
        }

        var password = Password.Create(request.Password);

        if (password.IsError)
        {
            return Error.Conflict();    // TODO: Zwrócenie niepoprawnego błędu - powiniene dotyczyć hasłą
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