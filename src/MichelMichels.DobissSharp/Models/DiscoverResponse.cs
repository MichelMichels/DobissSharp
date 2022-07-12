using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Models
{
    public record DiscoverResponse
    {
        [JsonPropertyName("groups")]
        public IList<Group> Groups { get; set; }

        [JsonPropertyName("temp_calendars")]
        public IList<object> TemperatureCalendars { get; set; }

        [JsonPropertyName("audio_sources")]
        public IList<object> AudioSources { get; set; }
    }
}
