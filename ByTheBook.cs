using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Testing;

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
            if(this.Hand.Count() == 2)
            {
                var value = this.CurrentValue().First();
                if(value == 9)
                {
                    if (dealersTopCard.Value >= 2 && dealersTopCard.Value <= 6)
                        return PlayAction.Double;
                }
                if (value == 10)
                {
                    if (dealersTopCard.Value >= 2 && dealersTopCard.Value <= 9)
                        return PlayAction.Double;
                }
                if (value == 11)
                {
                    if (dealersTopCard.CardFace != CardFace.Ace)
                        return PlayAction.Double;
                }
                if(this.CurrentValue().Any(x => x == 12))
                {
                    if (dealersTopCard.Value >= 5 && dealersTopCard.Value <= 10 || dealersTopCard.CardFace == CardFace.Ace)
                        return PlayAction.Double;
                    
                }

            }


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