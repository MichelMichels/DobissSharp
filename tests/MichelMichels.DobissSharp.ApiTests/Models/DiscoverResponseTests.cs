using MichelMichels.DobissSharp.Api.Models;

namespace MichelMichels.DobissSharp.Api.Tests.Models;

[TestClass]
public class DiscoverResponseTests
{
    [TestMethod]
    public void Init()
    {
        // Arrange

        // Act
        DiscoverResponse discoverResponse = new();

        // Assert
        Assert.IsNotNull(discoverResponse.Groups);
        Assert.IsNull(discoverResponse.TemperatureCalendars);
        Assert.IsNull(discoverResponse.AudioSources);
    }
}
