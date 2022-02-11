using ErgoHandle.Core.Models.ErgoHandle.Api;

namespace ErgoHandle.Core.Interfaces;

/// <summary>Interface for service with token burning functionality</summary>
public interface ITokenBurner
{
        /// <returns>Transaction ID</returns>
    Task<string?> BurnTokensAsync(IEnumerable<BurnTokensRequest> tokensToBurn);
}