using BepInEx.Logging;

namespace UnshadedCitadel.Utils;

public static class Log
{
    private static ManualLogSource LogSource;

    public static void Init(ManualLogSource logSource)
    {
        LogSource = logSource;
    }

    public static void Debug(object data) => LogSource.LogDebug(data);

    public static void Info(object data) => LogSource.LogInfo(data);

    public static void Warning(object data) => LogSource.LogWarning(data);

    public static void Error(object data) => LogSource.LogError(data);
}
