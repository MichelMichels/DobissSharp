using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Api.Models;
using MichelMichels.DobissSharp.Comparers;
using MichelMichels.DobissSharp.Enums;
using MichelMichels.DobissSharp.Models;
using System.Text.Json;

namespace MichelMichels.DobissSharp;

public class DobissService(
    IDobissClient dobissClient) : IDobissService
{
    private readonly IDobissClient dobissClient = dobissClient ?? throw new ArgumentNullException(nameof(dobissClient));

    private List<FormattedStatusResponse> cachedFormattedStatusReponse = [];
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
            await GetStatusAll();
        }

        return cachedFormattedStatusReponse.First(x => x.AddressId == element.AddressId).StatusByChannelId[element.ChannelId];
    }

    private async Task GetStatusAll()
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
            .Select(x =>
            {
                return (DobissDeviceType)x.IconsId switch
                {
                    _ => new DobissElement(x),
                };
            })
            .ToList());

        foreach (Group? group in _discovery.Groups.Where(x => x.GroupInfo is not null && x.GroupInfo.Id != 0))
        {
            List<DobissElement> elements = [];
            foreach (Subject subject in group.Subjects)
            {
                DobissElement? nxtElement = _elements.FirstOrDefault(x => x.AddressId == subject.Address && x.ChannelId == subject.Channel);
                if (nxtElement is not null)
                {
                    elements.Add(nxtElement);
                }
            }

            var addedGroup = new DobissGroup((string)group.GroupInfo!.Name, elements);
            _groups.Add(addedGroup);

        }

        _isInitialized = true;
    }

    Task IDobissService.GetStatus(DobissElement element)
    {
        throw new NotImplementedException();
    }

    public Task TurnOn(DobissLight light)
    {
        throw new NotImplementedException();
    }

    public Task TurnOff(DobissLight light)
    {
        throw new NotImplementedException();
    }

    public Task Toggle(DobissLight light)
    {
        throw new NotImplementedException();
    }
}