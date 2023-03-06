using Domain.Blockades;
using Domain.Blockades.ValueObjects;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class BlockadeConfiguration : IEntityTypeConfiguration<Blockade>
{
    public void Configure(EntityTypeBuilder<Blockade> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, x => BlockadeId.CreateFrom(x));

        builder
            .Property(x => x.BlockedAt)
            .IsRequired()
            .HasConversion(x => x.Value, x => BlockedAt.Create(x));

        builder
            .Property(x => x.BlockadeRelease)
            .HasConversion(x => x.Value, x => BlockadeRelease.Create(x));

        builder
            .Property(x => x.BlockadeReason)
            .IsRequired()
            .HasConversion(x => x.Value, x => BlockadeReason.Create(x).Value);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(x => x.CreatedBy)
            .HasConversion(x => x.Value, x => UserId.CreateFrom(x))
            .IsRequired();

        builder
            .Property(x => x.LastModifiedBy)
            .HasConversion(x => x == null ? Guid.Empty : x.Value, x => UserId.CreateFrom(x));
    }
}