namespace Application.Users.Common;

public sealed record AuthenticationResult(
    string Email,
    string Username,
    string Token,
    Guid RefreshToken,
    DateTime ExpiresAt);