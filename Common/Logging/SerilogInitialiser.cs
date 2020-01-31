using System;
using System.IO;
using System.Reflection;
using Serilog;

namespace Common.Logging
{
    public static class SerilogInitialiser
    {
        public static void Initialise(LoggingConfiguration loggingConfig, bool isDevelopment)
        {
            var loggerConfiguration = new LoggerConfiguration();

            if (isDevelopment)
            {
                var logLocation = $"{loggingConfig.LogFilePath}";
                loggerConfiguration = loggerConfiguration
                    .MinimumLevel.Is(loggingConfig.LogLevel)
                    .WriteTo.Console()
                    .WriteTo.File(logLocation, rollingInterval: RollingInterval.Day);
            }
            else
            {
                if (!string.IsNullOrEmpty(loggingConfig.LogEntriesToken))
                {
                    // Configure a live log management tool such as 'Kibana' for example
                    // -- Example --
                    // loggerConfiguration = loggerConfiguration
                    //         .MinimumLevel.Is(logConfig.LogLevel)
                    //         .WriteTo.Async(config => config.Logentries(logConfig.LogEntriesToken));
                }
            }
            
            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}