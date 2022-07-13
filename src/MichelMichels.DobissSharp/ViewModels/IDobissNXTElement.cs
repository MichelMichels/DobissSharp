using MichelMichels.DobissSharp.Enums;

namespace MichelMichels.DobissSharp.ViewModels
{
    public interface IDobissNXTElement
    {
        int ModuleId { get; set; }
        int ChannelId { get; set; }
        string Name { get; set; }
        NXTElementType ElementType { get; set; }
        DeviceType DeviceType { get; set; }
    }
}
