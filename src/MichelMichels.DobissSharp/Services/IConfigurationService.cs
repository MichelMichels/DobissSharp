using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Services
{
    public interface IConfigurationService
    {
        string Get(string key);
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}
