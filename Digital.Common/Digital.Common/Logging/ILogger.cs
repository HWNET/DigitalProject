using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Common.Logging
{
    public interface ILogger
    {
        void Log(object message);
    }
}
