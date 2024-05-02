namespace MichelMichels.DobissSharp.Enums;

public enum DobissAction
{
    Off = 0,
    On = 1,
    Toggle = 2,
    StartDimming = 3,
    StopDimming = 4,
    BlinkAndOn = 5,
    BlinkAndOff = 6,
    BlinkAndInitialState = 7,
    // Action 8 is undefined/unknown
    OnByPIR = 9,
    OnMilliseconds = 10,
    SkipSource = 104,
    ActivateCalendar = 110
}
