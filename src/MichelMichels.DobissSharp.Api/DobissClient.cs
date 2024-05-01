using MichelMichels.DobissSharp.Api.Exceptions;
using MichelMichels.DobissSharp.Api.Models;
using MichelMichels.DobissSharp.Api.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MichelMichels.DobissSharp.Api;

public class DobissClient(DobissClientOptions options) : IDobissClient
{
    private readonly DobissClientOptions options = options ?? throw new ArgumentNullException(nameof(options));

    private HttpClient? _httpClient;
    private IJwtTokenGenerator? _jwtTokenGenerator;

    /// <summary>
    /// Wrapper method for GET request /api/local/discover
    /// </summary>
    /// <returns></returns>
    public async Task<DiscoverResponse> Discover()
    {
        HttpClient httpClient = GetOrCreateHttpClient();

        string requestUri = "api/local/discover";
        HttpResponseMessage message = await httpClient.GetAsync(requestUri);

        return await ParseContent<DiscoverResponse>(message);
    }

    /// <summary>
    /// Wrapper method for POST request /api/local/action
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public async Task<ActionResponse> Action(ActionRequest body)
    {
        HttpClient httpClient = GetOrCreateHttpClient();

        string requestUri = "api/local/action";
        HttpResponseMessage message = await httpClient.PostAsJsonAsync(requestUri, body);
        return await ParseContent<ActionResponse>(message);
        //await ThrowsOnWrongSecretKey(async () =>
        //{
        //    var request = new RestRequest("api/local/action");
        //    request.AddJsonBody(body);

        //    var response = await _restClient.PostAsync(request);
        //    Debug.WriteLine($"Action executed, answer: {response.Content}");
        //});
    }

    /// <summary>
    /// Wrapper method for GET request /api/local/status
    /// </summary>
    /// <returns></returns>
    public async Task<StatusResponse> Status()
    {
        HttpClient httpClient = GetOrCreateHttpClient();

        string requestUri = "api/local/status";
        HttpResponseMessage message = await httpClient.GetAsync(requestUri);

        return await ParseContent<StatusResponse>(message);
    }

    private IJwtTokenGenerator GetOrCreateJwtTokenGenerator()
    {
        return _jwtTokenGenerator ??= new DobissJwtTokenGenerator("MichelMichels.DobissSharp");
    }
    private HttpClient GetOrCreateHttpClient()
    {
        IJwtTokenGenerator jwtTokenGenerator = GetOrCreateJwtTokenGenerator();

        HttpClient httpClient = _httpClient ??= new HttpClient()
        {
            BaseAddress = new Uri(options.BaseUrl),

        };

        if (httpClient.DefaultRequestHeaders.Authorization is null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenGenerator.GenerateToken(options.SecretKey));
        }

        return httpClient;
    }
    private static async Task<T> ParseContent<T>(HttpResponseMessage message) where T : class
    {
        if (message.IsSuccessStatusCode)
        {
            T result = await message.Content.ReadFromJsonAsync<T>() ?? throw new DobissSharpApiException("Serialized result was null.");
            return result;
        }
        else
        {
            throw new DobissSharpApiException($"Statuscode {message.StatusCode}, Message: '{message.Content.ReadAsStringAsync()}'");
        }
    }
}