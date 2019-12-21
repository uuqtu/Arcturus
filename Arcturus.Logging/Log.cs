using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.Logging
{
    public sealed class Log
    {
        private static readonly Log instance = new Log();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Log()
        {
        }

        private Log()
        {
        }

        public static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

    }
}
