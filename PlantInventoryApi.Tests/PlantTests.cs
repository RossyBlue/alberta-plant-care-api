using PlantInventoryApi.Models;

namespace PlantInventoryApi.Tests;

public class PlantTests
{
    [Theory]
    [InlineData("Living Room Window", true)]
    [InlineData("living room WINDOW", true)]
    [InlineData("Draft zone near the door", true)]
    [InlineData("Kitchen Shelf", false)]
    public void IsDraftRisk_DependsOnLocation(string location, bool expected)
    {
        var plant = new Plant { LocationInHouse = location };

        Assert.Equal(expected, plant.IsDraftRisk);
    }
}