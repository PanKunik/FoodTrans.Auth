namespace Application.Users.Commands.MeCommand;

public sealed record MeDTO(
    string FirstName,
    string LastName,
    string Email,
    string Username);