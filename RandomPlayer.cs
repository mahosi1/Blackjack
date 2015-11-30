using System;
using System.Diagnostics;

namespace Blackjack
{
    [DebuggerDisplay("{Name} - {TheHand}")]
    public class RandomPlayer : Player
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        public RandomPlayer(string name)
            : base(name)
        {
        }

        public override PlayAction Play(Card dealersTopCard)
        {
            var returnValue = (PlayAction)random.Next(6);
            if (returnValue == PlayAction.DoubleOrHit)
            {
                if (Hand.CanDouble)
                {
                    returnValue = PlayAction.Double;
                }
                else
                {
                    returnValue = PlayAction.Hit;
                }
            }
            else if (returnValue == PlayAction.DoubleOrStay)
            {
                if (Hand.CanDouble)
                {
                    returnValue = PlayAction.Double;
                }
                else
                {
                    returnValue = PlayAction.Stay;
                }
            }
            if (returnValue == PlayAction.Double && !Hand.CanDouble)
            {
                returnValue = PlayAction.Hit;
            }
            return returnValue;
        }
    }
}