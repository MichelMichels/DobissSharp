using MichelMichels.DobissSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Api
{
    public interface IDobissRestClient
    {
        Task<DiscoverResponse> Discover();
        Task<StatusResponse> Status();
        Task Status(int address, int channel);
        Task Action(ActionRequest actionRequest);
    }
}
