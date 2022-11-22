using Domain.Common.Models;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users.ValueObjects;
using ErrorOr;

namespace Domain.RefreshTokens;

public sealed class RefreshToken : AggregateRoot<RefreshTokenId>
{
    public Token Token { get; }
    public ExpiresAt ExpiresAt { get; }

    private RefreshToken(
        RefreshTokenId id,
        Token token,
        ExpiresAt expiresAt,
        DateTime createdAt,
        UserId createdBy,
        DateTime? lastModificationDate,
        UserId? lastModifiedBy)
        : base(id)
    {
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModificationDate = lastModificationDate;
        LastModifiedBy = lastModifiedBy;
    }

    public static ErrorOr<RefreshToken> Create(
        Token token,
        ExpiresAt expiresAt,
        Guid userId)
    {
        return new RefreshToken(
            RefreshTokenId.CreateUnique(),
            token,
            expiresAt,
            DateTime.UtcNow,
            Domain.Users.ValueObjects.UserId.CreateUnique(),
            null,
            null);
    }

    public bool IsExpired() => ExpiresAt <= DateTime.UtcNow;
}