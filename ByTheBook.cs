using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication7
{
    [DebuggerDisplay("{Name} - {TheHand}")]
    public class ByTheBook : Player
    {
        public ByTheBook(string name) : base(name)
        {
        }

        public override PlayAction Play(Card dealersTopCard)
        {
            IEnumerable<int> myHand = this.CurrentValue().ToArray();
            if (myHand.Any(x => x >= 17))
                return PlayAction.Stay;
            if (myHand.Any(x => x <= 10))
                return PlayAction.Hit;
            if (dealersTopCard.Value <= 6 && dealersTopCard.CardFace != CardFace.Ace)
            {
                return PlayAction.Stay;
            }
            return PlayAction.Hit;
        }
    }
}