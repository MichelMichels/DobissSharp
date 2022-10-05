using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.Services;

namespace MichelMichels.DobissSharp.ViewModels
{
    public class Light : DobissNXTElement
    {
        private readonly IDobissLightController controller;

        public Light(Subject subject, IDobissLightController controller) : base(subject)
        {
            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        public void Toggle() => controller.Toggle(this);
        public void TurnOff() => controller.TurnOff(this);
        public void TurnOn() => controller.TurnOn(this);      
    }
}
