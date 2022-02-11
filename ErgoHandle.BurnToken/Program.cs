using ErgoHandle.Core;
using ErgoHandle.Core.Models.ErgoHandle.Api;
using ErgoHandle.Core.Services;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                    .AddJsonFile("burnTokenConfig.json", true)
                    .AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .Build();

var nodeUrl = config["nodeUrl"];
var apiKey = config["apiKey"];
var tokensToBurn = config.GetSection("tokensToBurn").Get<BurnTokensRequest[]>();

if (
    string.IsNullOrEmpty(nodeUrl) ||
    string.IsNullOrEmpty(apiKey) ||
    tokensToBurn.Length == 0
)
{
    Console.WriteLine("One of these values was missing: nodeUrl | apiKey | tokenIds");
}

var tokenBurner = new TokenBurner(new ErgoNodeClient(nodeUrl, apiKey));
var txId = await tokenBurner.BurnTokensAsync(tokensToBurn);
Console.WriteLine($"txId: {txId}");