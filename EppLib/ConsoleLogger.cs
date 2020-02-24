using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EppLib
{
    class ConsoleLogger : IDebugger
    {
        public void Log(byte[] bytes)
        {
            LogMessageToConsole(Encoding.UTF8.GetString(bytes));
        }

        public void Log(string str)
        {
            LogMessageToConsole(str);
        }

        private static void LogMessageToConsole(string msg)
        {
            var logLine = string.Format(CultureInfo.InvariantCulture, "{0:G}: {1}.", DateTime.Now, msg);

            Console.WriteLine(logLine);
        }
    }
}
