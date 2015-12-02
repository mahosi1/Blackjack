namespace Blackjack
{
    class NullLogger : ILogger
    {
        public void WriteLine(string msg, params object[] vals)
        {
            
        }
    }
}