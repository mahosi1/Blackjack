using System;

namespace Blackjack
{
    public class RandomStrategy : IPlayerStrategy
    {
        readonly Random _random = new Random((int)DateTime.Now.Ticks);

        public PlayAction Play(Hand hand, Card dealersTopCard)
        {
            var returnValue = (PlayAction)_random.Next(6);
            if (returnValue == PlayAction.DoubleOrHit)
            {
                returnValue = hand.CanDouble ? PlayAction.Double : PlayAction.Hit;
            }
            else if (returnValue == PlayAction.DoubleOrStay)
            {
                returnValue = hand.CanDouble ? PlayAction.Double : PlayAction.Stay;
            }
            if (returnValue == PlayAction.Double && !hand.CanDouble)
            {
                returnValue = PlayAction.Hit;
            }
            return returnValue;
        }
    }
}