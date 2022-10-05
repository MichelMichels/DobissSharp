using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Services
{
    public class DobissLightController : IDobissLightController
    {
        private readonly IDobissRestClient restClient;

        public DobissLightController(IDobissRestClient restClient)
        {
            this.restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task Toggle(Light light)
        {
            await restClient.Action(new Models.ActionRequest()
            {
                ActionId = (int)Enums.Action.Toggle,
                AddressId = light.AddressId,
                ChannelId = light.ChannelId,
            });
        }

        public async Task TurnOff(Light light)
        {
            await restClient.Action(new Models.ActionRequest()
            {
                ActionId = (int)Enums.Action.Off,
                AddressId = light.AddressId,
                ChannelId = light.ChannelId,
            });
        }

        public async Task TurnOn(Light light)
        {
            await restClient.Action(new Models.ActionRequest()
            {
                ActionId = (int)Enums.Action.On,
                AddressId = light.AddressId,
                ChannelId = light.ChannelId,
            });
        }
    }
}
