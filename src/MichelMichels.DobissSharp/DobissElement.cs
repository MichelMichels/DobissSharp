using MichelMichels.DobissSharp.Api.Models;
using MichelMichels.DobissSharp.Enums;

namespace MichelMichels.DobissSharp;

public class DobissElement
{
    public DobissElement(Subject subject)
    {
        Name = subject.Name;
        AddressId = subject.Address;
        ChannelId = subject.Channel;
        ElementType = (DobissElementType)subject.Type;
        DeviceType = (DobissDeviceType)subject.IconsId;
    }

    public int AddressId { get; set; }
    public int ChannelId { get; set; }
    public string Name { get; set; }
    public DobissElementType ElementType { get; set; }
    public DobissDeviceType DeviceType { get; set; }
}
