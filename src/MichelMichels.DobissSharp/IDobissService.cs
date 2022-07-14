using MichelMichels.DobissSharp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp
{
    public interface IDobissService
    {
        Task<List<IRoom>> GetRooms();
        Task<List<IDobissNXTElement>> GetNXTElements();
    }
}
