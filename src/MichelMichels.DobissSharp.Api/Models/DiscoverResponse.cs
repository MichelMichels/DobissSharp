using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public record DiscoverResponse
{
    [JsonPropertyName("groups")]
    public List<Group> Groups { get; set; } = [];

    [JsonPropertyName("temp_calendars")]
    public object? TemperatureCalendars { get; set; } = null;

    [JsonPropertyName("audio_sources")]
    public object? AudioSources { get; set; } = null;
}
