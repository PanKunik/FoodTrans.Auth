using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public sealed class FoodTransAuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public FoodTransAuthDbContext(DbContextOptions<FoodTransAuthDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}