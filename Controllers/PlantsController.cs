using Microsoft.AspNetCore.Mvc;
using PlantInventoryApi.Models;
using PlantInventoryApi.Services;

namespace PlantInventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantsController : ControllerBase
{
    private readonly IPlantRepository _repository;

    public PlantsController(IPlantRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetPlants() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlant(int id)
    {
        var plant = await _repository.GetByIdAsync(id);
        if (plant == null)
            return NotFound(new { message = "Planta no encontrada en tu inventario." });

        return Ok(plant);
    }

    [HttpGet("alberta-winter-care")]
    public async Task<IActionResult> GetWinterCareAlerts()
    {
        var needyPlants = await _repository.GetHighCareWinterPlantsAsync();
        return Ok(new
        {
            Note = "Atención: El aire en Alberta en invierno requiere humidificador (<20% humedad) para estas plantas.",
            Plants = needyPlants
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlant([FromBody] PlantRequestDto dto)
    {
        var plant = await _repository.AddAsync(MapToPlant(dto));
        return CreatedAtAction(nameof(GetPlant), new { id = plant.Id }, plant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlant(int id, [FromBody] PlantRequestDto dto)
    {
        var updated = await _repository.UpdateAsync(id, MapToPlant(dto));
        return updated
            ? NoContent()
            : NotFound(new { message = "Planta no encontrada en tu inventario." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlant(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted
            ? NoContent()
            : NotFound(new { message = "Planta no encontrada en tu inventario." });
    }

    private static Plant MapToPlant(PlantRequestDto dto) => new()
    {
        Name = dto.Name,
        LocationInHouse = dto.LocationInHouse,
        RequiresHumidifier = dto.RequiresHumidifier,
        NeedsGrowLight = dto.NeedsGrowLight,
        WinterWateringDaysInterval = dto.WinterWateringDaysInterval,
        LastWatered = ToUtc(dto.LastWatered)
    };

    // Npgsql solo acepta DateTime en UTC
    private static DateTime ToUtc(DateTime value) => value.Kind switch
    {
        DateTimeKind.Utc => value,
        DateTimeKind.Local => value.ToUniversalTime(),
        _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
    };
}