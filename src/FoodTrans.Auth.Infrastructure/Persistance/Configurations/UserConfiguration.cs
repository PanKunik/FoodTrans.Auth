using Domain.User;
using Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.CreateUnique());

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .Property(x => x.Email)
            .HasConversion(x => x.Value, x => Email.Create(x).Value)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasIndex(x => x.Username)
            .IsUnique();

        builder
            .Property(x => x.Username)
            .HasConversion(x => x.Value, x => Username.Create(x).Value)
            .HasMaxLength(50);

        builder
            .Property(x => x.Password)
            .HasConversion(x => x.Value, x => Password.Create(x).Value)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.FirstName)
            .HasConversion(x => x.Value, x => FirstName.Create(x).Value)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.LastName)
            .HasConversion(x => x.Value, x => LastName.Create(x).Value)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Active)
            .IsRequired();

        builder
            .Property(x => x.LastLogin);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(x => x.CreatedBy)
            .IsRequired();
    }
}