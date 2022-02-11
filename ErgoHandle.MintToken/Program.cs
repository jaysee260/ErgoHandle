using ErgoHandle.Core;
using ErgoHandle.Core.Services;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                    .AddJsonFile("mintTokenConfig.json", true)
                    .AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .Build();

var nodeUrl = config["nodeUrl"];
var apiKey = config["apiKey"];
var tokenName = config["tokenName"];
var tokenAmount = int.Parse(config["tokenAmount"]);
var tokenDecimals = int.Parse(config["tokenDecimals"]);
var tokenDescription = config["tokenDescription"];
var receiverAddress = config["receiverAddress"]; // TODO: Maybe make this required?

if (
    string.IsNullOrEmpty(nodeUrl) ||
    string.IsNullOrEmpty(apiKey) ||
    string.IsNullOrEmpty(tokenName) ||
    string.IsNullOrEmpty(tokenDescription)
)
{
    Console.WriteLine("One of these values was missing: nodeUrl | apiKey | tokenName | tokenDescription");
}

var tokenMinter = new TokenMinter(new ErgoNodeClient(nodeUrl, apiKey));
var txId = await tokenMinter.MintTokenAsync(tokenName, tokenAmount, tokenDecimals, tokenDescription, receiverAddress);
Console.WriteLine($"txId: {txId}");