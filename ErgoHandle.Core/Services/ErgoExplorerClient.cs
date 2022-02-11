using System.Text.Json;
using ErgoHandle.Core.Models;
using ErgoHandle.Core.Utils;

namespace ErgoHandle.Core.Services;

public class ErgoExplorerClient
{
    public const string MAINNET_EXPLORER_URL = "https://api.ergoplatform.com/api/v1/";
    public const string TESTNET_EXPLORER_URL = "https://api-testnet.ergoplatform.com/api/v1/";

    private readonly HttpClient _explorerApiClient;

    public ErgoExplorerClient(NetworkType networkType)
    {
        _explorerApiClient = networkType switch
        {
            NetworkType.Mainnet => HttpClientBuilder.BuildExplorerHttpClient(MAINNET_EXPLORER_URL),
            NetworkType.Testnet => HttpClientBuilder.BuildExplorerHttpClient(TESTNET_EXPLORER_URL),
            _ => throw new ArgumentOutOfRangeException(nameof(networkType))
        };
    }

    public async Task<T> GetAsync<T>(string queryString)
    {
        var response = await _explorerApiClient.GetAsync(queryString);
        var serializedResponseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(serializedResponseContent);
        return result;
    }
}