using Domain.Blockades;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public sealed class FoodTransAuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Blockade> Blockades { get; set; }

    public FoodTransAuthDbContext(DbContextOptions<FoodTransAuthDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}