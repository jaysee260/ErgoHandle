namespace ErgoHandle.Core.Models.Node;

public class NodeAssetIssueRequest
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Decimals { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}