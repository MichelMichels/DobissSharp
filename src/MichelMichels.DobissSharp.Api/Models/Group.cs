using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public record Group
{
    [JsonPropertyName("group")]
    public GroupInfo? GroupInfo { get; set; } = null;

    [JsonPropertyName("subjects")]
    public List<Subject> Subjects { get; set; } = [];
}
