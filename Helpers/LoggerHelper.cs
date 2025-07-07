using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace MyApp.Utils
{
    public static class LoggerHelper
    {
        public static void LogDebug(ILogger logger, string message, [CallerMemberName] string methodName = "")
        {
            logger.LogDebug("{Method}: {Message}", methodName, message);
        }
    }
}