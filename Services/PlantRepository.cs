using PlantInventoryApi.Models;

namespace PlantInventoryApi.Services;

public class PlantRepository
{
    // Usamos un Array fijo simple en lugar de List
    private static Plant[] _plants = new Plant[]
    {
        new Plant 
        { 
            Id = 1, 
            Name = "Pothos (Devil's Ivy)", 
            LocationInHouse = "Kitchen Shelf", 
            RequiresHumidifier = false, 
            NeedsGrowLight = false, 
            WinterWateringDaysInterval = 14, 
            LastWatered = DateTime.Now.AddDays(-5) 
        },
        new Plant 
        { 
            Id = 2, 
            Name = "Calathea", 
            LocationInHouse = "Living Room Window", 
            RequiresHumidifier = true, 
            NeedsGrowLight = true, 
            WinterWateringDaysInterval = 7, 
            LastWatered = DateTime.Now.AddDays(-8) 
        }
    };

    public Plant[] GetAll()
    {
        return _plants;
    }

    public Plant? GetById(int id)
    {
        return _plants.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Plant plant)
    {
        plant.Id = _plants.Length + 1;
        _plants = _plants.Append(plant).ToArray();
    }

    public Plant[] GetHighCareWinterPlants()
    {
        return _plants.Where(p => p.RequiresHumidifier || p.NeedsGrowLight).ToArray();
    }
}