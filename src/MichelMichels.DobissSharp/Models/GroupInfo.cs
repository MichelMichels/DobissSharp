using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Models
{
    public record GroupInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icons_id ")]
        public int IconsId { get; set; }

        [JsonPropertyName("color")]
        public object Color { get; set; }

        [JsonPropertyName("image")]
        public object Image { get; set; }

        [JsonPropertyName("weight")]
        public string Weight { get; set; }
    }
}
