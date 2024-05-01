using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public record StatusResponse
{
    [JsonPropertyName("status")]
    public Dictionary<string, object> Statuses { get; set; } = [];
}
