using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public class Delay
{
    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public int Value { get; set; }
}
