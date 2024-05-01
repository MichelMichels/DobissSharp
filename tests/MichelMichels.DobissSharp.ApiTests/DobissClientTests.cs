using MichelMichels.DobissSharp.Api.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MichelMichels.DobissSharp.Api.Tests;


[TestClass]
public class DobissClientTests
{
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
    };

    [TestMethod, TestCategory("Local")]
    public async Task DiscoverTest()
    {
        // Arrange
        DobissClientOptions options = new()
        {
            BaseUrl = @"http://dobiss.local/",
            SecretKey = Environment.GetEnvironmentVariable("DOBISS_SECRET_KEY", EnvironmentVariableTarget.User) ?? string.Empty,
        };
        DobissClient client = new(options);

        // Act
        DiscoverResponse response = await client.Discover();

        // Assert
        Assert.IsNotNull(response);
        DebugWriteJson(response);
    }

    [TestMethod, TestCategory("Local")]
    public async Task StatusTest()
    {
        // Arrange
        DobissClientOptions options = new()
        {
            BaseUrl = @"http://dobiss.local/",
            SecretKey = Environment.GetEnvironmentVariable("DOBISS_SECRET_KEY", EnvironmentVariableTarget.User) ?? string.Empty,
        };
        DobissClient client = new(options);

        // Act
        StatusResponse response = await client.Status();

        // Assert
        Assert.IsNotNull(response);
        DebugWriteJson(response);
    }

    [TestMethod, TestCategory("Local")]
    public async Task ActionTest()
    {
        // Arrange
        DobissClientOptions options = new()
        {
            BaseUrl = @"http://dobiss.local/",
            SecretKey = Environment.GetEnvironmentVariable("DOBISS_SECRET_KEY", EnvironmentVariableTarget.User) ?? string.Empty,
        };
        DobissClient client = new(options);

        // Act
        ActionRequest body = new()
        {
            AddressId = 0,
            ChannelId = 12,
            ActionId = ActionId.Toggle,
        };
        ActionResponse response = await client.Action(body);

        // Assert
        Assert.IsNotNull(response);
        DebugWriteJson(response);
    }

    private static void DebugWriteJson(object value)
    {
        Debug.WriteLine(JsonSerializer.Serialize(value, _serializerOptions));
    }
}
