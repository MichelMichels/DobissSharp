using MichelMichels.DobissSharp.Exceptions;
using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.Services;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Text.Json;
using System.Net;

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
            var request = new RestRequest("api/local/discover");

            try
            {
                var response = await _restClient.GetAsync(request);
                return JsonSerializer.Deserialize<DiscoverResponse>(response.Content);
            } catch(HttpRequestException hre)
            {
                if (hre.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WrongSecretKeyException("Wrong secret key", hre);
                }

                throw;
            }
        }

        // GET /api/local/status
        public async Task<StatusResponse> Status()
        {
            return await _restClient.GetJsonAsync<StatusResponse>("api/local/status");
        }
    }
}