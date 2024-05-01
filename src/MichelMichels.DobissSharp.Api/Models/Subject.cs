using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public record Subject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public int Address { get; set; }

    [JsonPropertyName("channel")]
    public int Channel { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonPropertyName("icons_id")]
    public int IconsId { get; set; }

    [JsonPropertyName("dimmable")]
    public object? Dimmable { get; set; } = null;

    [JsonPropertyName("settings")]
    public object? Settings { get; set; } = null;
}
