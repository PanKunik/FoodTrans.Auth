using ErrorOr;
using FoodTrans.Auth.Domain.Entities;
using MediatR;

namespace FoodTrans.Auth.Application.Users.Commands;

public sealed class RegisterCommand : IRequest<ErrorOr<User>>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            request.Email,
            request.UserName,
            request.FirstName,
            request.LastName,
            request.Password);

        return await Task.FromResult(user);
    }
}