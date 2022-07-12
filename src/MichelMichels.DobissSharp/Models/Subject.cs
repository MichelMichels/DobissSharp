using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Models
{
    public record Subject
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public int Address { get; set; }

        [JsonPropertyName("channel")]
        public int Channel { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("tags")]
        public string Tags { get; set; }

        [JsonPropertyName("icons_id")]
        public int IconsId { get; set; }

        [JsonPropertyName("dimmable")]
        public object Dimmable { get; set; }
        
        [JsonPropertyName("settings")]
        public object Settings { get; set; }
    }
}
