using System;
using System.Runtime.CompilerServices;

namespace DeloitteDigital.Atlas.Logging
{
    public interface ILogService
    {
        void Debug(string message);
        void Debug(string message, object owner);

        void Info(string message);
        void Info(string message, object owner);

        void Warn(string message);
        void Warn(string message, object owner);

        void Error(string message);
        void Error(string message, object owner);
        void Error(string message, Exception exception);
        void Error(string message, Exception exception, object owner);

        void Fatal(string message);
        void Fatal(string message, object owner);

        /// <summary>
        /// Create a log scope wrapper that automatically logs the start and end of the code within the scope and the execution duration.
        /// The log entries are logged at the INFO level unless specified.
        /// </summary>
        /// <param name="ownerInstance">The calling instance. If supplied this will provide the full type information for the log record.</param>
        /// <param name="callerMemberName">The calling method or member name. Please leave blank to automatically be populated by the [CallerMemberName] attribute.</param>
        /// <param name="message">An optional additional message to log.</param>
        /// <param name="logLevel">The logging level. If left blank it will be Info</param>
        /// <returns>A LogScope</returns>
        LogScope WithLogScope(object ownerInstance = null, [CallerMemberName] string callerMemberName = "", string message = "", LogLevel logLevel = LogLevel.Info);
    }
}
