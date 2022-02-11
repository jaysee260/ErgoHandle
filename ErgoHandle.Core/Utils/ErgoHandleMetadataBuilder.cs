using System.Text.RegularExpressions;

namespace ErgoHandle.Core.Utils;

public static class ErgoHandleMetadataBuilder
{
    private static string MetadataTemplate = @"
        {{
            ""721"": {{
                ""{0}"": {{
                    ""name"": ""${1}"",
                    ""image"": ""{2}"",
                    ""description"": ""{3}"",
                    ""core"": {{
                        ""handleEncoding"": ""utf-8"",
                        ""prefix"": ""$"",
                        ""version"": 0,
                        ""officialMintAddresses"": [{4}]
                    }}
                }}
            }}
        }}
    ".TrimStart();
    public static string BuildTokenMetadata(string tokenName, string tokenImageUrl, string tokenDescription, string[] officialMintAddresses)
    {
        var formattedAddresses = officialMintAddresses.Select(addy => $"\"{addy}\"");
        var metadata = string.Format(MetadataTemplate, tokenName, tokenName, tokenImageUrl, tokenDescription, string.Join(',', formattedAddresses));
        // minify json string - https://stackoverflow.com/a/8913186/7516853
        return Regex.Replace(metadata, @"(""(?:[^""\\]|\\.)*"")|\s+", "$1");
    }
}