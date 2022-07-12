using MichelMichels.DobissSharp.Enums;
using MichelMichels.DobissSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.ViewModels
{
    public class DobissNXTElement : IDobissNXTElement
    {
        public DobissNXTElement(Subject subject)
        {
            Name = subject.Name;
            ModuleId = subject.Address;
            ChannelId = subject.Channel;
            Type = (NXTElementType)subject.Type;
            Device = (DeviceType)subject.IconsId;
        }               

        public int ModuleId { get; set; }
        public int ChannelId { get; set; }
        public string Name { get; set; }
        public NXTElementType Type { get; set; }
        public DeviceType Device { get; set; }
    }
}
