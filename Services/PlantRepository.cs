using Microsoft.EntityFrameworkCore;
using PlantInventoryApi.Data;
using PlantInventoryApi.Models;

namespace PlantInventoryApi.Services;

public class PlantRepository : IPlantRepository
{
    private readonly PlantDbContext _context;

    public PlantRepository(PlantDbContext context) => _context = context;

    public Task<List<Plant>> GetAllAsync() =>
        _context.Plants.AsNoTracking().ToListAsync();

    public Task<Plant?> GetByIdAsync(int id) =>
        _context.Plants.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    // Ajusta el filtro a la lógica que ya tenías en tu GetHighCareWinterPlants
    public Task<List<Plant>> GetHighCareWinterPlantsAsync() =>
        _context.Plants.AsNoTracking()
            .Where(p => p.RequiresHumidifier || p.NeedsGrowLight)
            .ToListAsync();

    public async Task<Plant> AddAsync(Plant plant)
    {
        _context.Plants.Add(plant);
        await _context.SaveChangesAsync();   // aquí Postgres asigna el Id
        return plant;
    }

    public async Task<bool> UpdateAsync(int id, Plant updated)
    {
        var existing = await _context.Plants.FindAsync(id);
        if (existing is null) return false;

        existing.Name = updated.Name;
        existing.LocationInHouse = updated.LocationInHouse;
        existing.RequiresHumidifier = updated.RequiresHumidifier;
        existing.NeedsGrowLight = updated.NeedsGrowLight;
        existing.WinterWateringDaysInterval = updated.WinterWateringDaysInterval;
        existing.LastWatered = updated.LastWatered;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant is null) return false;

        _context.Plants.Remove(plant);
        await _context.SaveChangesAsync();
        return true;
    }
}