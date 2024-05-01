using MichelMichels.DobissSharp.Api.Models;
using MichelMichels.DobissSharp.Enums;

namespace MichelMichels.DobissSharp.ViewModels;

public class DobissNXTElement
{
    public DobissNXTElement(Subject subject)
    {
        Name = subject.Name;
        AddressId = subject.Address;
        ChannelId = subject.Channel;
        ElementType = (NXTElementType)subject.Type;
        DeviceType = (DeviceType)subject.IconsId;
    }

    public int AddressId { get; set; }
    public int ChannelId { get; set; }
    public string Name { get; set; }
    public NXTElementType ElementType { get; set; }
    public DeviceType DeviceType { get; set; }
}
