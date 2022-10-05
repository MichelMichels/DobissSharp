using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Models
{
    public class Delay
    {
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
