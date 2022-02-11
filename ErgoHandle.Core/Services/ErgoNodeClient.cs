using System.Net.Http.Json;
using System.Text.Json;
using ErgoHandle.Core.Utils;

namespace ErgoHandle.Core.Services;

public class ErgoNodeClient
{
    public readonly string _nodeUrl;
    public readonly string _apiKey;
    private readonly HttpClient _nodeApiClient;

    public ErgoNodeClient(string nodeUrl, string apiKey)
    {
        _nodeUrl = nodeUrl ?? throw new ArgumentNullException(nameof(nodeUrl));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

        _nodeApiClient = HttpClientBuilder.BuildNodeHttpClient(nodeUrl, apiKey);
    }

    // TODO: Maybe add some concrete methods for checking wallet status, and to unlock it if it is locked.

    public async Task<TResponse> GetAsync<TResponse>(string uri)
    {
        var response = await _nodeApiClient.GetAsync(uri);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TResponse>(serializedResponseContent);
        return result;
    }

    public async Task<TReponse> PostAsync<TRequest, TReponse>(string uri, TRequest requestBody)
    {
        var response = await _nodeApiClient.PostAsJsonAsync<TRequest>(uri, requestBody);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        /*
         * if serializedResponseContent.Contains("wallet is locked"), throw new ErgoNodeWalletLockedException();
         */
        var result = JsonSerializer.Deserialize<TReponse>(serializedResponseContent);
        return result;
    }
}