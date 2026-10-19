using Microsoft.AspNetCore.Mvc;
using Moq;
using PlantInventoryApi.Controllers;
using PlantInventoryApi.Models;
using PlantInventoryApi.Services;

namespace PlantInventoryApi.Tests;

public class PlantsControllerTests
{
    private readonly Mock<IPlantRepository> _repo = new();
    private readonly PlantsController _controller;

    public PlantsControllerTests()
    {
        _controller = new PlantsController(_repo.Object);
    }

    private static PlantRequestDto ValidDto() => new()
    {
        Name = "Monstera",
        LocationInHouse = "Living room window",
        WinterWateringDaysInterval = 10,
        LastWatered = DateTime.UtcNow
    };

    [Fact]
    public async Task GetPlant_ReturnsNotFound_WhenPlantDoesNotExist()
    {
        _repo.Setup(r => r.GetByIdAsync(9999)).ReturnsAsync((Plant?)null);

        var result = await _controller.GetPlant(9999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetPlant_ReturnsOk_WhenPlantExists()
    {
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Plant { Id = 1, Name = "Pothos" });

        var result = await _controller.GetPlant(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Pothos", ((Plant)ok.Value!).Name);
    }

    [Fact]
    public async Task CreatePlant_ReturnsCreated()
    {
        _repo.Setup(r => r.AddAsync(It.IsAny<Plant>()))
             .ReturnsAsync((Plant p) => { p.Id = 5; return p; });

        var result = await _controller.CreatePlant(ValidDto());

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, created.StatusCode);
    }

    [Fact]
    public async Task CreatePlant_ConvertsLastWateredToUtc()
    {
        Plant? captured = null;
        _repo.Setup(r => r.AddAsync(It.IsAny<Plant>()))
             .Callback<Plant>(p => captured = p)
             .ReturnsAsync((Plant p) => p);

        var dto = ValidDto();
        dto.LastWatered = new DateTime(2026, 10, 15, 12, 0, 0, DateTimeKind.Unspecified);

        await _controller.CreatePlant(dto);

        Assert.Equal(DateTimeKind.Utc, captured!.LastWatered.Kind);
    }

    [Fact]
    public async Task UpdatePlant_ReturnsNoContent_WhenUpdated()
    {
        _repo.Setup(r => r.UpdateAsync(1, It.IsAny<Plant>())).ReturnsAsync(true);

        Assert.IsType<NoContentResult>(await _controller.UpdatePlant(1, ValidDto()));
    }

    [Fact]
    public async Task UpdatePlant_ReturnsNotFound_WhenPlantDoesNotExist()
    {
        _repo.Setup(r => r.UpdateAsync(9999, It.IsAny<Plant>())).ReturnsAsync(false);

        Assert.IsType<NotFoundObjectResult>(await _controller.UpdatePlant(9999, ValidDto()));
    }

    [Fact]
    public async Task DeletePlant_ReturnsNoContent_WhenDeleted()
    {
        _repo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        Assert.IsType<NoContentResult>(await _controller.DeletePlant(1));
    }

    [Fact]
    public async Task DeletePlant_ReturnsNotFound_WhenPlantDoesNotExist()
    {
        _repo.Setup(r => r.DeleteAsync(9999)).ReturnsAsync(false);

        Assert.IsType<NotFoundObjectResult>(await _controller.DeletePlant(9999));
    }
}