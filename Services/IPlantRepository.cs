using PlantInventoryApi.Models;

namespace PlantInventoryApi.Services;

public interface IPlantRepository
{
    Task<List<Plant>> GetAllAsync();
    Task<Plant?> GetByIdAsync(int id);
    Task<List<Plant>> GetHighCareWinterPlantsAsync();
    Task<Plant> AddAsync(Plant plant);
    Task<bool> UpdateAsync(int id, Plant updated);
    Task<bool> DeleteAsync(int id);
}