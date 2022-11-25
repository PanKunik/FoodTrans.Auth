namespace Application.Users.Common;

public sealed record AuthenticationResult(
    string Email,
    string Username,
    string Token,
    DateTime ExpiresAt);