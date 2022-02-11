using ErgoHandle.Core.Interfaces;
using ErgoHandle.Core.Models.Explorer;
using ErgoHandle.Core.Services;

namespace ErgoHandle.Core;

public class TokenValidator : ITokenValidator
{
    public readonly ErgoExplorerClient _explorerClient;

    public TokenValidator(ErgoExplorerClient explorerClient)
    {
        _explorerClient = explorerClient ?? throw new ArgumentNullException(nameof(explorerClient));
    }

    public async Task<IEnumerable<ExplorerAssetResponse>> SearchTokenByNameAsync(string tokenName)
    {
        var uri = $"tokens/bySymbol/{tokenName}";
        var results = await _explorerClient.GetAsync<IEnumerable<ExplorerAssetResponse>>(uri);
        return results;
    }

    public async Task<ExplorerBoxResponse> LookUpIssuanceBox(ExplorerAssetResponse token)
    {
        var issuanceBoxUri = $"boxes/{token.BoxId}";
        var issuanceBox = await _explorerClient.GetAsync<ExplorerBoxResponse>(issuanceBoxUri);
        return issuanceBox;
    }

    public async Task<ExplorerTransactionResponse> LookUpMintTx(ExplorerBoxResponse box)
    {
        var mintTxUri = $"transactions/{box.TransactionId}";
        var mintTx = await _explorerClient.GetAsync<ExplorerTransactionResponse>(mintTxUri);
        return mintTx;
    }

    public bool MintTxWasCreatedByMintAddress(ExplorerTransactionResponse mintTx, string mintAddress)
    {
        return mintTx.Inputs.First().Address == mintAddress;
    }

    public async Task<string?> ResolveTokenAddressAsync(ExplorerAssetResponse token)
    {
        var unspentBoxUri = $"boxes/unspent/byTokenId/{token.Id}";
        var unspentBox = await _explorerClient.GetAsync<ExplorerResponseWrapper<ExplorerBoxResponse>>(unspentBoxUri);
        var holderAddress = unspentBox.Items.Any() ? unspentBox.Items.First().Address : null;
        return holderAddress;
    }

    public Task<bool> TokenHasBeenBurnedAsync(ExplorerAssetResponse token)
    {
        throw new NotImplementedException();
    }

}