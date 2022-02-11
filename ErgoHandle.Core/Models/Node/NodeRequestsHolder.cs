namespace ErgoHandle.Core.Models.Node;

public class NodeRequestsHolder<T>
{
    public int Fee { get; set; }
    public IEnumerable<T> Requests { get; set; }
}