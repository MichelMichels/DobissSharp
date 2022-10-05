using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Models
{
    public record DiscoverResponse
    {
        [JsonPropertyName("groups")]
        public IList<Group> Groups { get; set; }

        [JsonPropertyName("temp_calendars")]
        public object TemperatureCalendars { get; set; }

        [JsonPropertyName("audio_sources")]
        public object AudioSources { get; set; }
    }
}
