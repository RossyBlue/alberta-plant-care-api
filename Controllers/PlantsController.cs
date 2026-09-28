using Microsoft.AspNetCore.Mvc;
using PlantInventoryApi.Models;
using PlantInventoryApi.Services;

namespace PlantInventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantsController : ControllerBase
{
    private readonly PlantRepository _repository;

    public PlantsController()
    {
        _repository = new PlantRepository();
    }

    [HttpGet]
    public IActionResult GetPlants()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetPlant(int id)
    {
        var plant = _repository.GetById(id);
        if (plant == null) 
            return NotFound(new { message = "Planta no encontrada en tu inventario." });
            
        return Ok(plant);
    }

    [HttpGet("alberta-winter-care")]
    public IActionResult GetWinterCareAlerts()
    {
        var needyPlants = _repository.GetHighCareWinterPlants();
        return Ok(new { 
            Note = "Atención: El aire en Alberta en invierno requiere humidificador (<20% humedad) para estas plantas.",
            Plants = needyPlants 
        });
    }

    [HttpPost]
    public IActionResult CreatePlant([FromBody] Plant newPlant)
    {
        _repository.Add(newPlant);
        return CreatedAtAction(nameof(GetPlant), new { id = newPlant.Id }, newPlant);
    }
}