using System.Net.Http.Headers;

namespace ErgoHandle.Core.Utils;

public static class HttpClientBuilder
{
    public static HttpClient BuildNodeHttpClient(string baseUrl, string apiKey)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Add("api_key", apiKey);

        return httpClient;
    }

    public static HttpClient BuildExplorerHttpClient(string baseUrl)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        return httpClient;
    }
}