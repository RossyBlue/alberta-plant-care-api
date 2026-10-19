using Microsoft.EntityFrameworkCore;
using PlantInventoryApi.Data;
using PlantInventoryApi.Models;
using PlantInventoryApi.Services;

namespace PlantInventoryApi.Tests;

public class PlantRepositoryTests
{
    // Cada test usa su propia base de datos en memoria, así no se afectan entre sí
    private static PlantRepository CreateRepository()
    {
        var options = new DbContextOptionsBuilder<PlantDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new PlantRepository(new PlantDbContext(options));
    }

    private static Plant NewPlant(string name = "Pothos", bool humidifier = false, bool growLight = false) => new()
    {
        Name = name,
        LocationInHouse = "Kitchen Shelf",
        RequiresHumidifier = humidifier,
        NeedsGrowLight = growLight,
        WinterWateringDaysInterval = 14,
        LastWatered = DateTime.UtcNow
    };

    [Fact]
    public async Task AddAsync_AssignsAnId()
    {
        var repo = CreateRepository();

        var saved = await repo.AddAsync(NewPlant());

        Assert.True(saved.Id > 0);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenPlantDoesNotExist()
    {
        var repo = CreateRepository();

        Assert.Null(await repo.GetByIdAsync(9999));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPlants()
    {
        var repo = CreateRepository();
        await repo.AddAsync(NewPlant("A"));
        await repo.AddAsync(NewPlant("B"));

        var plants = await repo.GetAllAsync();

        Assert.Equal(2, plants.Count);
    }

    [Fact]
    public async Task GetHighCareWinterPlantsAsync_ReturnsOnlyPlantsNeedingHumidifierOrGrowLight()
    {
        var repo = CreateRepository();
        await repo.AddAsync(NewPlant("Easy"));
        await repo.AddAsync(NewPlant("Needs humidifier", humidifier: true));
        await repo.AddAsync(NewPlant("Needs light", growLight: true));

        var result = await repo.GetHighCareWinterPlantsAsync();

        Assert.Equal(2, result.Count);
        Assert.DoesNotContain(result, p => p.Name == "Easy");
    }

    [Fact]
    public async Task UpdateAsync_ChangesFields_WhenPlantExists()
    {
        var repo = CreateRepository();
        var saved = await repo.AddAsync(NewPlant());

        var updated = NewPlant("Renamed");
        updated.LocationInHouse = "Bedroom shelf";
        var result = await repo.UpdateAsync(saved.Id, updated);

        var fromDb = await repo.GetByIdAsync(saved.Id);
        Assert.True(result);
        Assert.Equal("Renamed", fromDb!.Name);
        Assert.Equal("Bedroom shelf", fromDb.LocationInHouse);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenPlantDoesNotExist()
    {
        var repo = CreateRepository();

        Assert.False(await repo.UpdateAsync(9999, NewPlant()));
    }

    [Fact]
    public async Task DeleteAsync_RemovesPlant()
    {
        var repo = CreateRepository();
        var saved = await repo.AddAsync(NewPlant());

        var result = await repo.DeleteAsync(saved.Id);

        Assert.True(result);
        Assert.Null(await repo.GetByIdAsync(saved.Id));
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenPlantDoesNotExist()
    {
        var repo = CreateRepository();

        Assert.False(await repo.DeleteAsync(9999));
    }
}