# Ergo Handles (tentative name, likely to change)

Proof of concept solution for resolving human readable strings to Ergo addresses. Name is tentative and likely to change.

### Brief overview of projects in this solution


### ErgoHandle.Core

Core library exposing address resolution functionality.

### ErgoHandle.ResolveTokenNameToAddress

Console app to test Ergo address resolution by token name. It consumes `ErgoHandle.Core`.

#### How to run
```
dotnet run -- --tokenName name_of_token_to_lookup --mintAddress 3WwBpgxoiMKoHrk67HcquMUdGWKovAynL8tUjZWuFp9fjcbussTr
```

Alternatively, you can set these values in `resolveTokenNameConfig.json`, and run the console app from an IDE.
```json
{
  "tokenName": "demo_token",
  "mintAddress": "3WwBpgxoiMKoHrk67HcquMUdGWKovAynL8tUjZWuFp9fjcbussTr"
}
```

#### Arguments
- tokenName: The name of the token.
- mintAddress: The address of the minting engine. In this example, it is the address of a node wallet I've been using to mint tokens and test address resolution. In reality, this would be the established official mint address of the service.

<br>

### ErgoHandle.MintToken

Console app for minting tokens using the Ergo node's REST API. Built to facilitate testing.

#### How to run
Set the values in `mintTokenConfig.json` and run from an IDE (instructions on how to run from command line pending).
```json
{
  "nodeUrl": "http://127.0.0.1:9052/",
  "apiKey": "NODE_API_KEY",
  "tokenName": "demo_token",
  "tokenAmount": 1,
  "tokenDecimals": 0,
  "tokenDescription": "A token minted for demo purposes",
  "receiverAddress": "3WwM8LKw18msCLqzEaGNAaRgS4FqFQka1vsUCWjNtvdM9UTGSAuy"
}
```

#### Arguments
- nodeUrl: The url of the Ergo node.
- apiKey: The API key to authenticate requests against the node.
- tokenName: The name of the token
- tokenAmoun: The issue amount (since it's an NFT, should be 1).
- tokenDecimals: The token's decimal places (since it's an NFT, should be 0).
- tokenDescription: A description of the token.
- receiverAddress: The Ergo address that should receive the newly minted token. If left empty, token will be sent to mint address.

<br>

### ErgoHandle.BurnToken

Console app for burning tokens using the Ergo node's REST API. Built to facilitate testing.

#### How to run
Set the values in `burnTokenConfig.json` and run from an IDE (instructions on how to run from command line pending).
```json
{
  "nodeUrl": "http://127.0.0.1:9052/",
  "apiKey": "NODE_API_KEY",
  "tokensToBurn": [
    {
      "tokenId": "8cfcc93de7f8de8836b6e8e381fba8699803164643cfc710e10325fe826454ee",
      "tokenAmount": 1
    }
  ]
}
```

#### Arguments
- nodeUrl: The url of the Ergo node.
- apiKey: The API key to authenticate requests against the node.
- tokensToBurn: Array of objects describing the token(s) to burn.
    - tokenId: ID of the token to burn.
    - tokenAmount: Amount of the given token to burn.