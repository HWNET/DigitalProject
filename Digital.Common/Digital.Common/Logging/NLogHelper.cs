using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Digital.Common.Logging
{
    public enum MessageLevel
    {
        Level1, //Information
        Level2, //Error
        Level3  //Waring
    }
    public class NLogHelper
    {
        const string msgTemplate = @"Module: {0}, Method: {1}(), MessageLevel: {2}, Message: {3}";
        private NLog.Logger _logger;

        public NLogHelper()
        {
            var aamInstallPath = AppDomain.CurrentDomain.BaseDirectory;

            //var dbName = ConfigurationManager.AppSettings["DBName"];

            //var plantName = dbName.Substring(dbName.IndexOf('P'));

            //var path = Path.Combine(aamInstallPath, @"Logs\Service\" + plantName + string.Format(@"\{0}\", service));

            LoggingConfiguration config = new LoggingConfiguration();

            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            consoleTarget.Layout = @"${shortdate} ${date:format=HH\:mm\:ss} ${message}";
            config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            fileTarget.FileName = aamInstallPath + @"Logs\Reports\" + "${shortdate}.txt";
            //fileTarget.Layout = @"${shortdate} ${date:format=HH\:mm\:ss}${newline}${message}";
            fileTarget.Layout = @"${shortdate} ${date:format=HH\:mm\:ss} ${message}";
            config.AddTarget("file", fileTarget);

            LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);

            LogManager.Configuration = config;

            _logger = LogManager.GetLogger(Guid.NewGuid().ToString());
        }

        public NLog.Logger Logger
        {
            get
            {
                return _logger;
            }
        }

        public void WriteInfo(string msg)
        {
            if (_logger == null)
                throw new NullReferenceException("logger instance is not initialized.");

            _logger.Debug(msg);
        }

        public void WriteInfo(string module, MessageLevel level, string msg)
        {
            var method = new System.Diagnostics.StackFrame(1).GetMethod().Name;
            var msgLevel = SetMessageLevel(level);

            WriteInfo(string.Format(msgTemplate, module, method, msgLevel, msg));
        }

        private string SetMessageLevel(MessageLevel level)
        {
            var msgLevel = string.Empty;
            if (level == MessageLevel.Level1)
            {
                msgLevel = "Information";
            }
            else if (level == MessageLevel.Level2)
            {
                msgLevel = "Error";
            }
            else if (level == MessageLevel.Level3)
            {
                msgLevel = "Waring";
            }

            return msgLevel;
        }
    }
}
