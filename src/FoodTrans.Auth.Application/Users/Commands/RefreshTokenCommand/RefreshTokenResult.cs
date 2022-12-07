namespace Application.Users.Commands.RefreshTokenCommand;

public sealed record RefreshTokenResult(
    string Token,
    Guid RefreshToken,
    DateTime ExpiresAt);