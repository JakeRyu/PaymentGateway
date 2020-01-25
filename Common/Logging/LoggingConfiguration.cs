using Serilog.Events;

namespace Common.Logging
{
    public class LoggingConfiguration
    {
        public string LogEntriesToken { get; set; }    // security key for a live log management monitoring
        public LogEventLevel LogLevel { get; set; }
        public string LogFilePath { get; set; }
    }
}