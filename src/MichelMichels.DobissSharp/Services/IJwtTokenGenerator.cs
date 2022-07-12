using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken();
    }
}
