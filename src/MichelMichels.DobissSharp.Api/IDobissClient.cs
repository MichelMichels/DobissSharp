using MichelMichels.DobissSharp.Api.Models;

namespace MichelMichels.DobissSharp.Api;

public interface IDobissClient
{
    Task<DiscoverResponse> Discover();
    Task<StatusResponse> Status();
    Task<ActionResponse> Action(ActionRequest body);
}
