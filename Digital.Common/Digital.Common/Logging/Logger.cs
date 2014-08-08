using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.Logging
{
    public class Logger
    {
        private static readonly ILogger _errorLogger;

        static Logger()
        {
            _errorLogger = new CommonLogger();
        }

        public static void Error(object message)
        {
            _errorLogger.Log(message);
        }
    }
}
