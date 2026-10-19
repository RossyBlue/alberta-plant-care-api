using System.ComponentModel.DataAnnotations;

namespace PlantInventoryApi.Models;

public class PlantRequestDto
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(200, MinimumLength = 2)]
    public string LocationInHouse { get; set; } = string.Empty;

    public bool RequiresHumidifier { get; set; }
    public bool NeedsGrowLight { get; set; }

    [Range(1, 60, ErrorMessage = "El intervalo de riego debe estar entre 1 y 60 días.")]
    public int WinterWateringDaysInterval { get; set; }

    public DateTime LastWatered { get; set; }
}