namespace ISO810_ERP.Config;

public static class Constants
{
    // Tag used to identify blacklisted tokens in the memory cache
    public const string BlackListedTokenTag = "jwt-token-blacklist";

    public const int MinNameLength = 3;

    public const int MinSymbolLength = 1;

    public const int MinPasswordLength = 4;

    public const int MinPasswordHashLength = 10;
}