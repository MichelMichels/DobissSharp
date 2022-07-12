using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Models
{
    public record Status
    {
        public Dictionary<string, object> Outputs { get; set; }
    }
}
