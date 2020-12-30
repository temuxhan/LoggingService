using System;
using System.Text.RegularExpressions;

namespace LoggingService.SharedKernel
{
    public class LogLevelParser
    {
        private const string PATTERN = @"(\[\w+\])";

        private static Regex regex = new Regex(PATTERN);
        public static LogLevel GetLogLevelFromMessage(string message)
        {
            var match = regex.Match(message);
            string value = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

            LogLevel result = LogLevel.info;

            if (match.Success)
            {
                object logLevel;
                result = Enum.TryParse(typeof(LogLevel), value, true, out logLevel) ? (LogLevel)logLevel : result;
            }

            return result;
        }

        public static string GetMessageWithoutLogLevel(string message)
        {
            return regex.Replace(message, string.Empty).TrimStart();
        }
    }
}