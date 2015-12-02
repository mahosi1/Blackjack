namespace Blackjack
{
    public interface IPlayerStrategy
    {
        PlayAction Play(Hand hand, Card dealersTopCard);
    }
}