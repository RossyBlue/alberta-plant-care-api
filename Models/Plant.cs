namespace PlantInventoryApi.Models;

public class Plant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;              // ej: "Monstera Deliciosa"
    public string LocationInHouse { get; set; } = string.Empty;   // ej: "Living room window"
    public bool RequiresHumidifier { get; set; }                // Cierto para el aire seco de Alberta (<20% humedad)
    public bool NeedsGrowLight { get; set; }                     // Necesita luz artificial en los días cortos de invierno
    public int WinterWateringDaysInterval { get; set; }         // Días entre riegos durante el invierno
    public DateTime LastWatered { get; set; }
    
    // Propiedad calculada: Alerta si la planta está cerca de una ventana muy fría en Alberta
    public bool IsDraftRisk => LocationInHouse.ToLower().Contains("window") || LocationInHouse.ToLower().Contains("draft");
}