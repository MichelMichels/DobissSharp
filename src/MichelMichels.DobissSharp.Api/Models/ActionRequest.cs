using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api.Models;

public class ActionRequest
{
    [JsonPropertyName("address")]
    public int AddressId { get; set; }

    [JsonPropertyName("channel")]
    public int ChannelId { get; set; }

    [JsonPropertyName("action")]
    public int ActionId { get; set; }

    [JsonPropertyName("option1")]
    public int Option1 { get; set; }

    [JsonPropertyName("option2")]
    public int Option2 { get; set; }

    [JsonPropertyName("delayOn")]
    public Delay? DelayOn { get; set; } = null;

    [JsonPropertyName("delayOff")]
    public Delay? DelayOff { get; set; } = null;

    [JsonPropertyName("condition")]
    public Condition? Condition { get; set; } = null;
}
