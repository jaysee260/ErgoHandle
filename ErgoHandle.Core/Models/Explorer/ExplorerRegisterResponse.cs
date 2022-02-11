using System.Text.Json.Serialization;

namespace ErgoHandle.Core.Models.Explorer;

public class ExplorerRegisterResponse
{
    [JsonPropertyName("serializedValue")]
    public string SerializedValue { get; set; }

    [JsonPropertyName("sigmaType")]
    public string SigmaType { get; set; }

    [JsonPropertyName("renderedValue")]
    public string RenderedValue { get; set; }
}