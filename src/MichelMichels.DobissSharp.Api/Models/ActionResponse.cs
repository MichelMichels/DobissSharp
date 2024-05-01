using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public class ActionResponse
{
    [JsonPropertyName("new_status")]
    public int NewStatus { get; set; }
}
