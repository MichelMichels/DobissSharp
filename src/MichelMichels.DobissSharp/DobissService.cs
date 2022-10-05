using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Comparers;
using MichelMichels.DobissSharp.Enums;
using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.Services;
using MichelMichels.DobissSharp.ViewModels;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace MichelMichels.DobissSharp
{
    public class DobissService : IDobissService
    {
        private readonly IDobissRestClient client;
        private readonly IDobissLightController dobissLightController;
        private readonly ILogger<DobissService> logger;

        private List<FormattedStatusResponse> cachedFormattedStatusReponse;
        private DateTime lastUpdated;
        private DiscoverResponse _discovery;
        private readonly List<DobissGroup> _groups = new();
        private readonly List<DobissNXTElement> _elements = new();
        private bool _isInitialized;

        public DobissService(
            IDobissRestClient dobissRestClient,
            IDobissLightController dobissLightController,
            ILogger<DobissService> logger)
        {
            this.client = dobissRestClient ?? throw new ArgumentNullException(nameof(dobissRestClient));
            this.dobissLightController = dobissLightController ?? throw new ArgumentNullException(nameof(dobissLightController));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<DobissGroup>> GetGroups()
        {
            if(!_isInitialized)
            {
                await Initialize();
            }           

            return _groups;
        }
        public async Task<List<DobissNXTElement>> GetOutputs()
        {
            if(!_isInitialized)
            {
                await Initialize();
            }

            return _elements.Where(x => x.AddressId < 200).ToList();
        }
        public async Task GetStatus(DobissNXTElement element)
        {
            if(cachedFormattedStatusReponse == null || (DateTime.Now - lastUpdated).TotalSeconds > 5)
            {
                await GetStatusAll();
            }

            var status = cachedFormattedStatusReponse.First(x => x.AddressId == element.AddressId).StatusByChannelId[element.ChannelId];
            logger.LogDebug($"{element.Name}: {status}");
        }
        private async Task GetStatusAll()
        {
            cachedFormattedStatusReponse ??= new List<FormattedStatusResponse>();

            var statusResponse = await client.Status();

            cachedFormattedStatusReponse.Clear();
            foreach (var kvp in statusResponse.Statuses)
            {
                var addressId = Convert.ToInt32(kvp.Key);
                if (addressId >= 200)
                {
                    continue;
                }

                var formattedStatusData = new FormattedStatusResponse(addressId);
                try
                {
                    var array = JsonSerializer.Deserialize<List<int>>((JsonElement)kvp.Value);

                    // TODO check if zero based indexing is right
                    for (int i = 0; i < array.Count; i++)
                    {
                        formattedStatusData.StatusByChannelId.Add(i, array[i]);
                    }
                }
                catch (JsonException)
                {
                    var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>((JsonElement)kvp.Value);

                    foreach (var channelKvp in dictionary)
                    {
                        formattedStatusData.StatusByChannelId.Add(int.Parse(channelKvp.Key), channelKvp.Value);
                    }
                }

                cachedFormattedStatusReponse.Add(formattedStatusData);
            }

            lastUpdated = DateTime.Now;
        }

        private async Task Initialize()
        {
            if (_discovery == null)
            {
                _discovery = await client.Discover();

                var distinctSubjects = _discovery.Groups
                    .Where(x => x.GroupInfo.Id != 0)
                    .SelectMany(x => x.Subjects)
                    .Distinct(new SubjectComparer())
                    .OrderBy(x => x.Address)
                    .ThenBy(x => x.Channel)
                    .ToList();

                _elements.Clear();
                _elements.AddRange(
                    distinctSubjects
                    .Select(x =>
                    {
                        return (DeviceType)x.IconsId switch
                        {
                            DeviceType.Light => new Light(x, dobissLightController),
                            _ => new DobissNXTElement(x),
                        };
                    })
                    .ToList());

                foreach (var group in _discovery.Groups.Where(x => x.GroupInfo.Id != 0))
                {              
                    var elements = new List<DobissNXTElement>();
                    foreach (var subject in group.Subjects)
                    {
                        var nxtElement = _elements.FirstOrDefault(x => x.AddressId == subject.Address && x.ChannelId == subject.Channel);
                        if (nxtElement != null)
                        {
                            elements.Add(nxtElement);
                        }
                    }

                    var addedGroup = new DobissGroup((string)group.GroupInfo.Name, elements);
                    _groups.Add(addedGroup);

                }
            }

            _isInitialized = true;
        }
    }
}