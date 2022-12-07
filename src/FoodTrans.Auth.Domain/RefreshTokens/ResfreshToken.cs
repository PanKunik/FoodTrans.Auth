using Domain.Common.Models;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users.ValueObjects;
using ErrorOr;

namespace Domain.RefreshTokens;

public sealed class RefreshToken : AggregateRoot<RefreshTokenId>
{
    public Token Token { get; }
    public ExpiresAt ExpiresAt { get; }
    public UserId UserId { get; }
    public WasUsed WasUsed { get; }

    private RefreshToken(
        RefreshTokenId id,
        Token token,
        ExpiresAt expiresAt,
        UserId userId,
        WasUsed wasUsed,
        DateTime createdAt,
        UserId createdBy,
        DateTime? lastModificationDate,
        UserId? lastModifiedBy)
        : base(id)
    {
        Token = token;
        ExpiresAt = expiresAt;
        UserId = userId;
        WasUsed = wasUsed;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModificationDate = lastModificationDate;
        LastModifiedBy = lastModifiedBy;
    }

    public static ErrorOr<RefreshToken> Create(
        Token token,
        ExpiresAt expiresAt,
        UserId userId)
    {
        return new RefreshToken(
            RefreshTokenId.CreateUnique(),
            token,
            expiresAt,
            userId,
            false,
            DateTime.UtcNow,
            UserId.CreateUnique(),
            null,
            null);
    }

    public void Use()
    {
        if (!WasUsed)
        {
            WasUsed.Use();
        }
    }

    public bool IsExpired() => ExpiresAt <= DateTime.UtcNow;
}