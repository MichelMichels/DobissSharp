using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Models
{
    public record Group
    {
        [JsonPropertyName("group")]
        public GroupInfo GroupInfo { get; set; }

        [JsonPropertyName("subjects")]
        public IList<Subject> Subjects { get; set; }
    }
}
