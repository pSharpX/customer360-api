using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI
{
    public class NLogManager
    {
        private ILogger _logger;

        public NLogManager(ILogger logger)
        {
            _logger = logger;
        }
        public void LogDebug(Exception ex)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Debug, _logger.Name, _logger.Name);
            SetLogEventInfo(theEvent);
            _logger.Log(theEvent);
        }

        public void LogError(Exception ex)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Error, _logger.Name, _logger.Name);
            SetLogEventInfo(theEvent);
            theEvent.Exception = ex;
            _logger.Log(theEvent);
        }
        public void LogFatal(Exception ex)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Fatal, _logger.Name, _logger.Name);
            SetLogEventInfo(theEvent);
            _logger.Log(theEvent);
        }

        public void LogInfo(string message)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, _logger.Name, message);
            SetLogEventInfo(theEvent);
            _logger.Log(theEvent);
        }

        public void LogTrace(string message)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Trace, _logger.Name, message);
            SetLogEventInfo(theEvent);
            _logger.Log(theEvent);
        }

        public void LogWarn(Exception ex)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Warn, _logger.Name, _logger.Name);
            SetLogEventInfo(theEvent);
            _logger.Log(theEvent);
        }

        private static void SetLogEventInfo(LogEventInfo theEvent)
        {
            NLogData nLogData = new NLogData();

            theEvent.Properties["user"] = "hundred-dev";
            theEvent.Properties["Browser"] = nLogData.Browser;
            theEvent.Properties["RequestUrl"] = nLogData.RequestUrl;
            theEvent.Properties["ErrorMessage"] = nLogData.ErrorMessage;
            theEvent.Properties["REMOTE_IP_ADDRESS"] = nLogData.RemoteIPAddress;
            theEvent.Properties["SERVER_IP_ADDRESS"] = nLogData.ServerIPAddress;
        }


    }
}
