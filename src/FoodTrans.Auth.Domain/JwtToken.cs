namespace Domain;

public sealed class JwtToken
{
    public string Value { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Guid RefreshToken { get; set; }
}