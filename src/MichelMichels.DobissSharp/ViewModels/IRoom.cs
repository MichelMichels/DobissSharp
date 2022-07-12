using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.ViewModels
{
    public interface IRoom
    {
        string Name { get; }
        List<IDobissNXTElement> Elements { get; }
    }
}
