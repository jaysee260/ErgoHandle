using ErgoHandle.Core.Interfaces;
using ErgoHandle.Core.Models.Explorer;
using System.Collections.Concurrent;

namespace ErgoHandle.Core;
public class TokenResolver : ITokenResolver
{
    private readonly string _mintAddress;
    private readonly ITokenValidator _tokenValidator;

    public TokenResolver(string mintAddress, ITokenValidator tokenValidator)
    {
        _mintAddress = mintAddress ?? throw new ArgumentNullException(nameof(mintAddress));
        _tokenValidator = tokenValidator ?? throw new ArgumentNullException(nameof(tokenValidator));
    }

    public async Task<string?> ResolveTokenNameToAddressAsync(string tokenName)
    {
        if (string.IsNullOrEmpty(tokenName)) return null;

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 3 // can be a config value (from file or env)
        };

        // create a map of <mintTxId, token>
        var mapOfTokenIdByMintTx = new ConcurrentDictionary<string, ExplorerAssetResponse>();

        // Search token by name
        var tokens = await _tokenValidator.SearchTokenByNameAsync(tokenName);
        if (!tokens.Any()) return null;

        // Get issuance box for each result
        var issuanceBoxes = new ConcurrentBag<ExplorerBoxResponse>();
        await Parallel.ForEachAsync(tokens, parallelOptions, async (token, cancellationToken) =>
        {
            var issuanceBox = await _tokenValidator.LookUpIssuanceBox(token);
            issuanceBoxes.Add(issuanceBox);
            mapOfTokenIdByMintTx.TryAdd(issuanceBox.TransactionId, token);
        });
        if (!issuanceBoxes.Any()) return null;

        // Get mint tx of every issuance box
        var mintTxs = new ConcurrentBag<ExplorerTransactionResponse>();
        await Parallel.ForEachAsync(issuanceBoxes, parallelOptions, async (box, cancellationToken) =>
        {
            var mintTx = await _tokenValidator.LookUpMintTx(box);
            mintTxs.Add(mintTx);
        });
        if (!mintTxs.Any()) return null;

        // Validate token authenticity.
        // Making it a list to account for edge case where an authentic, burned token by the same name is found.
        // If so, will be filtered in final stage.
        var authenticTokens = new ConcurrentBag<ExplorerAssetResponse>();
        Parallel.ForEach(mintTxs, parallelOptions, (tx, cancellationToken) =>
        {
            var mintTxWasCreatedByMintAddress = _tokenValidator.MintTxWasCreatedByMintAddress(tx, _mintAddress);
            if (mintTxWasCreatedByMintAddress)
            {
                var token = mapOfTokenIdByMintTx[tx.Id];
                authenticTokens.Add(token);
            };
        });

        // Resolve address of wallet holding the token.
        // If it is burned, no unspent box will be found and the result will be null.
        string? walletAddress = null;
        await Parallel.ForEachAsync(authenticTokens, parallelOptions, async (token, cancellationToken) =>
        {
            var response = await _tokenValidator.ResolveTokenAddressAsync(token);
            walletAddress = response ?? null;
        });

        return walletAddress;
    }
}