using System;
using log4net;

namespace DeloitteDigital.Atlas.Logging.Log4net
{
    public class Log4netService : ILogService
    {
        public Log4netService()
        {
            if (!log4net.LogManager.GetLoggerRepository().Configured)
            {
                log4net.Config.XmlConfigurator.Configure();
            }
        }

        public void Debug(string message)
        {
            this.Debug(message, this);
        }

        public void Debug(string message, object owner)
        {
            var log = LogManager.GetLogger(owner.GetType());
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public void Info(string message)
        {
            this.Info(message, this);
        }

        public void Info(string message, object owner)
        {
            var log = LogManager.GetLogger(owner.GetType());
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public void Warn(string message)
        {
            this.Warn(message, this);
        }

        public void Warn(string message, object owner)
        {
            var log = LogManager.GetLogger(owner.GetType());
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        public void Error(string message)
        {
            this.Error(message, this);
        }

        public void Error(string message, object owner)
        {
            this.Error(message, null, owner);
        }

        public void Error(string message, Exception exception)
        {
            this.Error(message, exception, this);
        }

        public void Error(string message, Exception exception, object owner)
        {
            var log = LogManager.GetLogger(owner.GetType());
            if (log.IsErrorEnabled)
            {
                log.Error(message, exception);
            }
        }

        public void Fatal(string message)
        {
            this.Fatal(message, this);
        }

        public void Fatal(string message, object owner)
        {
            var log = LogManager.GetLogger(owner.GetType());
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
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
