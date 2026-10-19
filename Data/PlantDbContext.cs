using Microsoft.EntityFrameworkCore;
using PlantInventoryApi.Models;

namespace PlantInventoryApi.Data;

public class PlantDbContext : DbContext
{
    public PlantDbContext(DbContextOptions<PlantDbContext> options) : base(options) { }

    public DbSet<Plant> Plants => Set<Plant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plant>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.LocationInHouse).IsRequired().HasMaxLength(200);
            entity.Ignore(p => p.IsDraftRisk);   // es calculada, no va a la BD
        });
    }
}