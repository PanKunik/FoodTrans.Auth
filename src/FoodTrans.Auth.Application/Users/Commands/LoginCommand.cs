using Application.Contracts;
using Domain.User;
using Domain.User.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands;

public sealed class LoginCommand : IRequest<ErrorOr<User>>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);    // TODO: Niepotrzebne tworzenie obiektu VO (walidacja jest zbędna przy Query?)

        if (email.IsError)
        {
            return Error.Conflict();    // TODO: Zwrócenie niepoprawnego błędu - powinien dotyczyć adresy email
        }

        return await _userRepository.GetUserByEmail(email.Value);
    }
}