namespace Application.Users.Commands.RegisterCommand;

public sealed record RegisterResult(
    string Email,
    string Username,
    string FirstName,
    string LastName);