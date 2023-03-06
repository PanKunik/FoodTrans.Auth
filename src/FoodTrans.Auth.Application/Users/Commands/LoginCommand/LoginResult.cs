namespace Application.Users.Commands.LoginCommand;

public sealed record LoginResult(
    string Username,
    string Email,
    string Token,
    DateTime TokenExpires,
    Guid RefreshToken);