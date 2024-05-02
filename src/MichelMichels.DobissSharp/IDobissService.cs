namespace MichelMichels.DobissSharp;

public interface IDobissService
{
    Task<List<DobissGroup>> GetGroups();
    Task<List<DobissElement>> GetOutputs();
    Task<object> GetStatus(DobissElement element);
}
