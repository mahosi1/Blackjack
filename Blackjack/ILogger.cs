namespace Blackjack
{
    public interface ILogger
    {

        void WriteLine(string msg, params object[] vals);
    }
}