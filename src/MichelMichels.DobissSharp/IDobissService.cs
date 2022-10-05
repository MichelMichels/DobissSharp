using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.ViewModels;

namespace MichelMichels.DobissSharp
{
    public interface IDobissService
    {
        Task<List<DobissGroup>> GetGroups();
        Task<List<DobissNXTElement>> GetOutputs();
        Task GetStatus(DobissNXTElement element);
    }
}
