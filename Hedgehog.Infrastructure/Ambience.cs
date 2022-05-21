namespace Hedgehog.Infrastructure;

public static class Ambience
{
    public static bool IsProduction =>
        Name.ToLowerInvariant() == "production";

    public static bool IsPreProduction =>
            Name.ToLowerInvariant() == "preproduction";

    public static bool IsDevelopment =>
        Name.ToLowerInvariant() == "development";

    public static bool IsTesting =>
        Name.ToLowerInvariant() == "test";

    public static bool IsDebug =>
        Name.ToLowerInvariant() == "debug";
        
    public static bool IsAny(AmbienceKind kind) =>
        kind.HasFlag(AmbienceKind.Production) && IsProduction
        ||
        kind.HasFlag(AmbienceKind.PreProduction) && IsPreProduction
        ||
        kind.HasFlag(AmbienceKind.Testing) && IsTesting
        ||
        kind.HasFlag(AmbienceKind.Development) && IsDevelopment
        ||
        kind.HasFlag(AmbienceKind.Debug) && IsDebug;

    public static string Name =>
        Environment
            .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        ??
        "Development";
}

[Flags]
public enum AmbienceKind
{
    Production = 0x01,
    PreProduction = 0x02,
    Testing = 0x04,
    Development = 0x08,
    Debug = 0x10
}