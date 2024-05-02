namespace MichelMichels.DobissSharp;

public interface IDobissService : IDobissLightController
{
    Task<List<DobissGroup>> GetGroups();
    Task<List<DobissElement>> GetOutputs();
    Task GetStatus(DobissElement element);
}
