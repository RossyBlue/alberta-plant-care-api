using Microsoft.EntityFrameworkCore;
using PlantInventoryApi.Models;

namespace PlantInventoryApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(PlantDbContext db)
    {
        if (await db.Plants.AnyAsync()) return;

        db.Plants.AddRange(
            new Plant
            {
                Name = "Pothos (Devil's Ivy)",
                LocationInHouse = "Kitchen Shelf",
                RequiresHumidifier = false,
                NeedsGrowLight = false,
                WinterWateringDaysInterval = 14,
                LastWatered = DateTime.UtcNow.AddDays(-5)
            },
            new Plant
            {
                Name = "Calathea",
                LocationInHouse = "Living Room Window",
                RequiresHumidifier = true,
                NeedsGrowLight = true,
                WinterWateringDaysInterval = 7,
                LastWatered = DateTime.UtcNow.AddDays(-8)
            });

        await db.SaveChangesAsync();
    }
}