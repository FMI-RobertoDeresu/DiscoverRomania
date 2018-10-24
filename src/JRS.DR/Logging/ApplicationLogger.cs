using System;
using JRS.DR.Contracts;
using NLog;

namespace JRS.DR.Logging
{
    public class ApplicationLogger : IApplicationLogger
    {
        private const string LoggerName = "ApplicationLogger";

        private readonly ILogger _logger;

        public ApplicationLogger()
        {
            _logger = LogManager.GetLogger(LoggerName);
        }

        public void LogError(Exception exception)
        {
            var errorLog = LogEventInfoFromException(exception, LogLevel.Error);
            _logger.Log(errorLog);
        }

        public void LogFatal(Exception exception)
        {
            var fatalLog = LogEventInfoFromException(exception, LogLevel.Fatal);
            _logger.Log(fatalLog);
        }

        private static LogEventInfo LogEventInfoFromException(Exception exception, LogLevel level)
        {
            var exceptionLog = new LogEventInfo(level, LoggerName, string.Empty);
            exceptionLog.Properties["Created"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            exceptionLog.Properties["Type"] = $"{level} - {exception.GetType().Name}";
            exceptionLog.Properties["Message"] = exception.Message;
            exceptionLog.Properties["Trace"] = exception.StackTrace;
            return exceptionLog;
        }
    }
}