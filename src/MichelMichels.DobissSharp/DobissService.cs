using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp
{
    public class DobissService : IDobissService
    {
        private readonly IDobissRestClient _client;
        private DiscoverResponse _discovery;
        private readonly List<IRoom> _rooms;
        private bool _isInitialized;

        public DobissService(IDobissRestClient client)
        {
            _client = client;
            _rooms = new List<IRoom>();
        }

        public async Task<List<IRoom>> GetRooms()
        {
            if(!_isInitialized)
            {
                await Initialize();
            }           

            return _rooms;
        }

        private async Task Initialize()
        {
            if (_discovery == null)
            {
                _discovery = await _client.Discover();

                var distinctSubjects = _discovery.Groups
                    .SelectMany(x => x.Subjects)
                    .Distinct()
                    .OrderBy(x => x.Address)
                    .ThenBy(x => x.Channel);

                var nxtElements = distinctSubjects.Select(x => new DobissNXTElement(x)).ToList();

                foreach (var group in _discovery.Groups.Where(x => x.GroupInfo.Id != 0))
                {
                    var room = new Room(group.GroupInfo.Name);
                    _rooms.Add(room);

                    foreach (var subject in group.Subjects)
                    {
                        var nxtElement = nxtElements.FirstOrDefault(x => x.ModuleId == subject.Address && x.ChannelId == subject.Channel);
                        if (nxtElement != null)
                        {
                            room.Elements.Add(nxtElement);
                        }
                    }
                }
            }

            _isInitialized = true;
        }
    }
}