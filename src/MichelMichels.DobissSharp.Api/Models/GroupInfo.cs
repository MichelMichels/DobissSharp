using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public record GroupInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("icons_id ")]
    public int IconsId { get; set; }

    [JsonPropertyName("color")]
    public object? Color { get; set; } = null;

    [JsonPropertyName("image")]
    public object? Image { get; set; } = null;

    [JsonPropertyName("weight")]
    public string? Weight { get; set; } = null;
}
