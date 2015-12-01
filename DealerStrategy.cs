namespace Blackjack
{
    public class DealerStrategy : IPlayerStrategy
    {
        public PlayAction Play(Hand hand, Card dealersTopCard)
        {
            if (null != hand.SoftValue)
            {
                if (hand.SoftValue.Value < 17)
                    return PlayAction.Hit;
            }
            if (hand.HardValue < 17)
            {
                return PlayAction.Hit;
            }
            return PlayAction.Stay;
        }
    }
}