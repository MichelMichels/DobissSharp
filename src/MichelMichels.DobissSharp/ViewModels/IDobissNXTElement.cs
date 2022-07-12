using MichelMichels.DobissSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.ViewModels
{
    public interface IDobissNXTElement
    {
        int ModuleId { get; set; }
        int ChannelId { get; set; }
        string Name { get; set; }
        NXTElementType Type { get; set; }
    }
}
