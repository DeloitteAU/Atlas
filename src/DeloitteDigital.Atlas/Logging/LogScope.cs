using System;
using System.Diagnostics;

namespace DeloitteDigital.Atlas.Logging
{
    /// <summary>
    /// Log scope automatically adds begin and end info log statements to the code it wraps.
    /// </summary>
    public class LogScope : IDisposable
    {
        private readonly ILogService log;
        private readonly string callerMemberName;
        private readonly LogLevel logLevel;
        private readonly string callerTypeName = "";
        private readonly Stopwatch stopWatch = new Stopwatch();

        public LogScope(ILogService log, string callerMemberName, string message, object ownerInstance, LogLevel logLevel)
        {
            this.stopWatch.Start();

            this.log = log;
            this.callerMemberName = callerMemberName;
            this.logLevel = logLevel;

            if (!string.IsNullOrWhiteSpace(message)) this.callerMemberName = this.callerMemberName + " - " + message;
            if (ownerInstance != null) this.callerTypeName = ownerInstance.GetType().FullName;

            Begin(this.callerMemberName, this.callerTypeName);
        }

        private volatile bool isDisposed;
        public void Dispose()
        {
            if (this.isDisposed) return;

            this.isDisposed = true;

            try
            {
                this.stopWatch.Stop();

                End(this.callerMemberName, this.callerTypeName, $"(took {this.stopWatch.ElapsedMilliseconds}ms)");
            }
            catch
            {
                // ignored
            }
        }

        private void Begin(string callerMemberName = "", string callerTypeName = "", string message = "")
        {
            var result = $"Starting {LogExtensions.GetFullTypeName(callerMemberName, callerTypeName)} {message}";

            Log(result);
        }

        private void End(string callerMemberName = "", string callerTypeName = "", string message = "")
        {
            var logMessage = $"Completed {LogExtensions.GetFullTypeName(callerMemberName, callerTypeName)} {message}";

            Log(logMessage);
        }

        private void Log(string message)
        {
            switch (this.logLevel)
            {
                case LogLevel.Debug:
                    this.log.Debug(message);
                    break;
                case LogLevel.Info:
                    this.log.Info(message);
                    break;
                case LogLevel.Warn:
                    this.log.Warn(message);
                    break;
                case LogLevel.Error:
                    this.log.Error(message);
                    break;
                case LogLevel.Fatal:
                    this.log.Fatal(message);
                    break;
                default:
                    this.log.Debug(message);
                    break;
            }
        }

        public string CallerMemberName { get { return this.callerMemberName; } }
        public string CallerTypeName { get { return this.callerTypeName; } }
        public string CallerFullName { get { return $"{this.CallerTypeName}.{this.CallerMemberName}"; } }
    }
}
