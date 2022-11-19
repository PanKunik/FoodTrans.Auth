using Domain.Common.Models;
using Domain.RefreshToken.ValueObjects;
using Domain.User.ValueObjects;
using ErrorOr;

namespace Domain.RefreshToken;

public sealed class RefreshToken : AggregateRoot<RefreshTokenId>
{
    public Token Token { get; }

    private RefreshToken(
        RefreshTokenId id,
        Token token,
        DateTime createdAt,
        UserId createdBy,
        DateTime? lastModificationDate,
        UserId? lastModifiedBy)
        : base(id)
    {
        Token = token;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModificationDate = lastModificationDate;
        LastModifiedBy = lastModifiedBy;
    }

    public static ErrorOr<RefreshToken> Create(
        Token token)
    {
        return new RefreshToken(
            RefreshTokenId.CreateUnique(),
            token,
            DateTime.UtcNow,
            UserId.CreateUnique(),
            null,
            null);
    }
}