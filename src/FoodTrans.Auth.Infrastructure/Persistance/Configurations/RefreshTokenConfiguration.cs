using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, _ => RefreshTokenId.CreateUnique());

        builder
            .Property(x => x.Token)
            .HasConversion(x => x.Value, x => Token.Create(x))
            .IsRequired();

        builder
            .Property(x => x.ExpiresAt)
            .HasConversion(x => x.Value, x => ExpiresAt.Create(x).Value);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(x => x.CreatedBy)
            .HasConversion(x => x.Value, _ => UserId.CreateUnique())
            .IsRequired();

        builder
            .Property(x => x.LastModifiedBy)
            .HasConversion(x => x == null ? Guid.Empty : x.Value, _ => UserId.CreateUnique());
    }
}