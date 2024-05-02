using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Api.Models;
using MichelMichels.DobissSharp.Comparers;
using MichelMichels.DobissSharp.Enums;
using MichelMichels.DobissSharp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MichelMichels.DobissSharp;

public class DobissService(
    IDobissClient dobissClient) : IDobissService, IDobissLightController
{
    private readonly IDobissClient dobissClient = dobissClient ?? throw new ArgumentNullException(nameof(dobissClient));

    private readonly List<FormattedStatusResponse> cachedFormattedStatusReponse = [];
    private DateTime? lastUpdated;
    private DiscoverResponse? _discovery;
    private readonly List<DobissGroup> _groups = [];
    private readonly List<DobissElement> _elements = [];
    private bool _isInitialized;

    public async Task<List<DobissGroup>> GetGroups()
    {
        await Initialize();
        return _groups;
    }
    public async Task<List<DobissElement>> GetOutputs()
    {
        await Initialize();
        return _elements.Where(x => x.AddressId < 200).ToList();
    }
    public async Task<object> GetStatus(DobissElement element)
    {
        if (cachedFormattedStatusReponse.Count == 0 || lastUpdated is null || (DateTime.Now - lastUpdated.Value).TotalSeconds > 5)
        {
            await LoadStatusIntoCache();
        }

        return cachedFormattedStatusReponse.First(x => x.AddressId == element.AddressId).StatusByChannelId[element.ChannelId];
    }

    private async Task LoadStatusIntoCache()
    {
        cachedFormattedStatusReponse.Clear();

        StatusResponse statusResponse = await dobissClient.Status();

        foreach (KeyValuePair<string, object> kvp in statusResponse.Statuses)
        {
            int addressId = Convert.ToInt32(kvp.Key);
            if (addressId >= 200)
            {
                continue;
            }

            FormattedStatusResponse formattedStatusData = new(addressId);
            try
            {
                List<int>? array = JsonSerializer.Deserialize<List<int>>((JsonElement)kvp.Value);

                // TODO check if zero based indexing is right
                for (int i = 0; i < array.Count; i++)
                {
                    formattedStatusData.StatusByChannelId.Add(i, array[i]);
                }
            }
            catch (JsonException)
            {
                Dictionary<string, object>? dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>((JsonElement)kvp.Value);

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
        if (_discovery is not null || _isInitialized)
        {
            return;
        }

        _discovery = await dobissClient.Discover();

        List<Subject> distinctSubjects = _discovery.Groups
            .Where(x => x.GroupInfo is not null && x.GroupInfo.Id != 0)
            .SelectMany(x => x.Subjects)
            .Distinct(new SubjectComparer())
            .OrderBy(x => x.Address)
            .ThenBy(x => x.Channel)
            .ToList();

        _elements.Clear();
        _elements.AddRange(
            distinctSubjects
            .Select(ConvertToDobissElement)
            .ToList());

        foreach (Group group in _discovery.Groups.Where(x => x.GroupInfo is not null && x.GroupInfo.Id != 0))
        {
            DobissGroup dobissGroup = new(group.GroupInfo!.Name);

            foreach (Subject subject in group.Subjects)
            {
                DobissElement? element = _elements.FirstOrDefault(x => x.AddressId == subject.Address && x.ChannelId == subject.Channel);
                if (element is null)
                {
                    continue;
                }

                dobissGroup.Elements.Add(element);
            }

            _groups.Add(dobissGroup);
        }

        _isInitialized = true;
    }

    public async Task TurnOn(DobissLight light)
    {
        ActionRequest request = GenerateActionRequest(light, ActionId.On);
        ActionResponse response = await dobissClient.Action(request);

        if (response.NewStatus != request.ActionId)
        {
            Debug.WriteLine($"New status not set!");
        }
    }
    public async Task TurnOff(DobissLight light)
    {
        ActionRequest request = GenerateActionRequest(light, ActionId.Off);
        ActionResponse response = await dobissClient.Action(request);

        if (response.NewStatus != request.ActionId)
        {
            Debug.WriteLine($"New status not set!");
        }
    }
    public async Task Toggle(DobissLight light)
    {
        ActionRequest request = GenerateActionRequest(light, ActionId.Toggle);
        ActionResponse response = await dobissClient.Action(request);

        if (response.NewStatus != request.ActionId)
        {
            Debug.WriteLine($"New status not set!");
        }
    }

    private DobissElement ConvertToDobissElement(Subject subject)
    {
        return (DobissDeviceType)subject.IconsId switch
        {
            DobissDeviceType.Light => new DobissLight(subject, this),
            _ => new DobissElement(subject),
        };
    }

    private static ActionRequest GenerateActionRequest(DobissElement element, int actionId)
    {
        return new ActionRequest()
        {
            ActionId = actionId,
            AddressId = element.AddressId,
            ChannelId = element.ChannelId,
        };
    }
}