using ErgoHandle.Core;
using ErgoHandle.Core.Models;
using ErgoHandle.Core.Services;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                    .AddJsonFile("resolveTokenNameConfig.json", true)
                    .AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .Build();

var tokenName = config["tokenName"];
var mintAddress = config["mintAddress"];
var tokenResolver = new TokenResolver(
    mintAddress,
    new TokenValidator(new ErgoExplorerClient(NetworkType.Testnet))
);

var startTime = DateTime.UtcNow;
var walletAddress = await tokenResolver.ResolveTokenNameToAddressAsync(tokenName);
Console.WriteLine($"Finished process in {DateTime.UtcNow.Subtract(startTime).TotalMilliseconds} ms");
Console.WriteLine(new { handle = tokenName, address = walletAddress });