using System.Diagnostics;

namespace Blackjack
{
    public class TraceLogger : ILogger
    {
        public void WriteLine(string msg, params object[] vals)
        {
            Trace.WriteLine(string.Format(msg, vals));
        }
    }
}
