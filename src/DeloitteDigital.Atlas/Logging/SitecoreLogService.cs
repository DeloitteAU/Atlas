using System;
using Sitecore.Diagnostics;

namespace DeloitteDigital.Atlas.Logging
{
    public class SitecoreLogService : ILogService
    {
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Debug(string message, object owner)
        {
            Log.Debug(message, owner);
        }

        public void Info(string message)
        {
            Log.Info(message, this);
        }

        public void Info(string message, object owner)
        {
            Log.Info(message, owner);
        }

        public void Warn(string message)
        {
            Log.Warn(message, this);
        }

        public void Warn(string message, object owner)
        {
            Log.Warn(message, owner);
        }

        public void Error(string message)
        {
            Log.Error(message, this);
        }

        public void Error(string message, object owner)
        {
            Log.Error(message, owner);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception, this);
        }

        public void Error(string message, Exception exception, object owner)
        {
            Log.Error(message, exception, owner);
        }

        public void Fatal(string message)
        {
            Log.Fatal(message, this);
        }

        public void Fatal(string message, object owner)
        {
            Log.Fatal(message, owner);
        }

        public LogScope WithLogScope(
            object ownerInstance = null,
            string callerMemberName = "",
            string message = "",
            LogLevel logLevel = LogLevel.Info)
        {
            return new LogScope(this, callerMemberName, message, ownerInstance, logLevel);
        }
    }
}
