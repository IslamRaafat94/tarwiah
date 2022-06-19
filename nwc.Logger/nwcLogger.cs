using NLog.Web;
using System;
using System.Runtime.CompilerServices;

namespace nwc.Logger
{
	public class nwcLogger : IDisposable
    {
        private static readonly NLog.Logger _logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        public static NLog.Logger Getlogger([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "", [CallerFilePath] string filepath = "")
        {
            return _logger
                .WithProperty("LineNumber", lineNumber)
                .WithProperty("MemberName", memberName)
                .WithProperty("FilePath", filepath);
        }
        public static void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }
        public static void Info(string message)
        {
            _logger.Info(message);
        }
        public static void Debug(string message)
        {
            _logger.Debug(message);
        }
        public static void Warning(string message)
        {
            _logger.Warn(message);
        }
        public static void Fatal(string message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }
        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
        public void LogTrace(string message)
        {
            _logger.Trace(message);
        }
        public void LogFatal(string message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }

        public void Dispose()
        {
            NLog.LogManager.Shutdown();
            this.Dispose();

        }
    }
}