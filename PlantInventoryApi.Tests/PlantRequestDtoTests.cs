using System.ComponentModel.DataAnnotations;
using PlantInventoryApi.Models;

namespace PlantInventoryApi.Tests;

public class PlantRequestDtoTests
{
    private static PlantRequestDto ValidDto() => new()
    {
        Name = "Monstera",
        LocationInHouse = "Living room window",
        RequiresHumidifier = true,
        NeedsGrowLight = false,
        WinterWateringDaysInterval = 10,
        LastWatered = DateTime.UtcNow
    };

    private static List<ValidationResult> Validate(PlantRequestDto dto)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(dto, new ValidationContext(dto), results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void ValidDto_HasNoErrors()
    {
        Assert.Empty(Validate(ValidDto()));
    }

    [Theory]
    [InlineData("")]
    [InlineData("x")]
    public void InvalidName_ReturnsError(string name)
    {
        var dto = ValidDto();
        dto.Name = name;

        var errors = Validate(dto);

        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(PlantRequestDto.Name)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(61)]
    public void WateringIntervalOutOfRange_ReturnsError(int days)
    {
        var dto = ValidDto();
        dto.WinterWateringDaysInterval = days;

        var errors = Validate(dto);

        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(PlantRequestDto.WinterWateringDaysInterval)));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(60)]
    public void WateringIntervalAtLimits_IsValid(int days)
    {
        var dto = ValidDto();
        dto.WinterWateringDaysInterval = days;

        Assert.Empty(Validate(dto));
    }
}