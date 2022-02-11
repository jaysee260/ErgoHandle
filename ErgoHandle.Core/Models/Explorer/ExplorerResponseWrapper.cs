using System.Text.Json.Serialization;

namespace ErgoHandle.Core.Models.Explorer;

public class ExplorerResponseWrapper<T>
{
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; }
    [JsonPropertyName("total")]
    public int Total { get; set; }
}