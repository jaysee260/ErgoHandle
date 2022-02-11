namespace ErgoHandle.Core.Interfaces;

/// <summary>Interface for service with token minting functionality</summary>
public interface ITokenMinter
{
    // Maybe add a method to check if a token with a given name (handle) has already been minted by a given wallet.

    /// <returns>Transaction Id</returns>
    Task<string?> MintTokenAsync(string tokenName, int tokenAmount, int tokenDecimals, string tokenDescription, string receiverAddress);
}