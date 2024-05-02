namespace MichelMichels.DobissSharp;

public interface IDobissLightController
{
    Task TurnOn(DobissLight light);
    Task TurnOff(DobissLight light);
    Task Toggle(DobissLight light);
}
