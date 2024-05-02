using MichelMichels.DobissSharp.Api.Models;

namespace MichelMichels.DobissSharp;

public class DobissLight(Subject subject, IDobissLightController controller) : DobissElement(subject)
{
    private readonly IDobissLightController controller = controller ?? throw new ArgumentNullException(nameof(controller));

    public Task TurnOn() => controller.TurnOn(this);
    public Task TurnOff() => controller.TurnOff(this);
    public Task Toggle() => controller.Toggle(this);
}
