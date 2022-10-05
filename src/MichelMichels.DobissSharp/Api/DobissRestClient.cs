using MichelMichels.DobissSharp.Exceptions;
using MichelMichels.DobissSharp.Models;
using MichelMichels.DobissSharp.Services;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Text.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace MichelMichels.DobissSharp.Api
{
    public class DobissRestClient : IDobissRestClient
    {
        private readonly RestClient _restClient;

        public DobissRestClient(
            IAuthenticator authenticator, 
            IConfigurationService configurationService)
        {
            var options = new RestClientOptions(configurationService.Get("endpoint"));

            _restClient = new RestClient(options)
            {
                Authenticator = authenticator,
            };
        }

        // POST /api/local/action
        public async Task Action(ActionRequest actionRequest)
        {
            await ThrowsOnWrongSecretKey(async () =>
            {
                var request = new RestRequest("api/local/action");
                request.AddJsonBody(actionRequest);

                var response = await _restClient.PostAsync(request);
                Debug.WriteLine($"Action executed, answer: {response.Content}");
            });
        }

        // GET /api/local/discover
        public async Task<DiscoverResponse> Discover()
        {
            return await ThrowsOnWrongSecretKey(async () =>
            {
                var request = new RestRequest("api/local/discover");
                var response = await _restClient.GetAsync(request);
                Debug.WriteLine($"GET /api/local/discover: {response.Content}");

                var options = new JsonSerializerOptions()
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                };
                return JsonSerializer.Deserialize<DiscoverResponse>(response.Content, options);
            });
        }

        // GET /api/local/status
        public async Task<StatusResponse> Status()
        {
            return await ThrowsOnWrongSecretKey(async () =>
            {
                return await _restClient.GetJsonAsync<StatusResponse>("api/local/status");
            });
        }

        // GET /api/local/status with address and channel parameter
        public async Task Status(int address, int channel)
        {
            await ThrowsOnWrongSecretKey(async () =>
            {
                var request = new RestRequest("api/local/status");
                request.AddJsonBody(new StatusRequestData()
                {
                    Address = address,
                    Channel = channel,
                    
                });
                var response = await _restClient.GetAsync(request);
                Debug.WriteLine(response.Content);
            });
        }

        private async Task ThrowsOnWrongSecretKey(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (HttpRequestException hre)
            {
                if (hre.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WrongSecretKeyException("Wrong secret key", hre);
                }

                throw;
            }
        }
        private async Task<T> ThrowsOnWrongSecretKey<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            } catch(HttpRequestException hre)
            {
                if (hre.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WrongSecretKeyException("Wrong secret key", hre);
                }

                throw;
            }
        }
    }
}