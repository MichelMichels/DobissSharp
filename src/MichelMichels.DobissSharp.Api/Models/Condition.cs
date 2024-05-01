using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public class Condition
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("operator")]
    public bool Operator { get; set; }
}
