using ErgoHandle.Core.Models.Node;

namespace ErgoHandle.Abstractions.Models.Node;

public class NodeBurnTokensRequest
{
    public IEnumerable<NodeAsset> AssetsToBurn { get; set; }
}