using MichelMichels.DobissSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.ViewModels
{
    public class Light : DobissNXTElement, ILight
    {
        public Light(Subject subject) : base(subject)
        {

        }

        public void Toggle()
        {
            throw new NotImplementedException();
        }

        public void TurnOff()
        {
            throw new NotImplementedException();
        }

        public void TurnOn()
        {
            throw new NotImplementedException();
        }
    }
}
