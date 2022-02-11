using ErgoHandle.Abstractions.Models.Node;
using ErgoHandle.Core.Interfaces;
using ErgoHandle.Core.Models.ErgoHandle.Api;
using ErgoHandle.Core.Models.Node;
using ErgoHandle.Core.Services;
using ErgoHandle.Core.Utils;

namespace ErgoHandle.Core;

public class TokenBurner : ITokenBurner
{
    public readonly ErgoNodeClient _nodeClient;

    public TokenBurner(ErgoNodeClient nodeClient)
    {
        _nodeClient = nodeClient ?? throw new ArgumentNullException(nameof(nodeClient));
    }

    public async Task<string?> BurnTokensAsync(IEnumerable<BurnTokensRequest> tokensToBurn)
    {
        var assetsToBurn = tokensToBurn.Select(token => new NodeAsset { TokenId = token.TokenId, Amount = token.TokenAmount });
        var burnTokensRequest = new NodeRequestsHolder<NodeBurnTokensRequest>
        {
            Fee = Ergo.MinFee,
            Requests = new[]
            {
                new NodeBurnTokensRequest
                {
                    AssetsToBurn = assetsToBurn
                }
            }
        };

        var txId = await _nodeClient.PostAsync<NodeRequestsHolder<NodeBurnTokensRequest>, string>("wallet/transaction/send", burnTokensRequest);
        return txId;
    }
}