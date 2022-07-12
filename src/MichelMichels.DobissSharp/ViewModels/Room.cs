using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.ViewModels
{
    public class Room : IRoom
    {
        public Room(string name)
        {
            Name = name;
            Elements = new List<IDobissNXTElement>();
        }
        
        public string Name { get;  }
        public List<IDobissNXTElement> Elements { get; }
    }
}
