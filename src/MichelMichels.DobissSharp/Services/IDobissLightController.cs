using MichelMichels.DobissSharp.ViewModels;

namespace MichelMichels.DobissSharp.Services
{
    public interface IDobissLightController
    {
        Task TurnOn(Light light);
        Task TurnOff(Light light);
        Task Toggle(Light light);
    }
}
