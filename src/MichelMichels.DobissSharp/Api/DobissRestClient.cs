using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.Services;
using RestSharp;
using RestSharp.Authenticators;

namespace MichelMichels.DobissSharp.Api
{
    public class DobissRestClient : IDobissRestClient
    {
        private readonly RestClient _restClient;

        public DobissRestClient(IAuthenticator authenticator, IConfigurationService configurationService)
        {
            var options = new RestClientOptions(configurationService.Get("endpoint"));

            _restClient = new RestClient(options)
            {
                Authenticator = authenticator,
            };
        }

        // POST /api/local/action
        public Task Action()
        {
            throw new NotImplementedException();
        }

        // GET /api/local/discover
        public async Task<DiscoverResponse> Discover()
        {
            return await _restClient.GetJsonAsync<DiscoverResponse>("api/local/discover");
        }

        // GET /api/local/status
        public async Task<StatusResponse> Status()
        {
            return await _restClient.GetJsonAsync<StatusResponse>("api/local/status");
        }
    }
}