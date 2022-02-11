using ErgoHandle.Core.Interfaces;
using ErgoHandle.Core.Models.Node;
using ErgoHandle.Core.Services;
using ErgoHandle.Core.Utils;

namespace ErgoHandle.Core;

public class TokenMinter : ITokenMinter
{
    public readonly ErgoNodeClient _nodeClient;

    public TokenMinter(ErgoNodeClient nodeClient)
    {
        _nodeClient = nodeClient ?? throw new ArgumentNullException(nameof(nodeClient));
    }

    public async Task<string?> MintTokenAsync(string tokenName, int tokenAmount, int tokenDecimals, string tokenDescription, string receiverAddress)
    {
        var assetIssueRequest = new NodeRequestsHolder<NodeAssetIssueRequest>
        {
            Fee = Ergo.MinFee,
            Requests = new[]
            {
                new NodeAssetIssueRequest
                {
                    Name = tokenName,
                    Amount = tokenAmount,
                    Decimals = tokenDecimals,
                    Description = tokenDescription
                }
            }
        };

        if (!string.IsNullOrEmpty(receiverAddress)) assetIssueRequest.Requests.First().Address = receiverAddress;

        var txId = await _nodeClient.PostAsync<NodeRequestsHolder<NodeAssetIssueRequest>, string>("wallet/transaction/send", assetIssueRequest);
        return txId;
    }
}