using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication7
{
    public abstract class CardHandler
    {
        

        public string ToStringOfHand()
        {
            var sb = new StringBuilder();
            foreach (Card card in Hand)
            {
                sb.AppendFormat("{0}-{1}\n", card.CardFace, card.Suit);
            }
            return sb.ToString();
        }

        protected readonly List<Card> _hand = new List<Card>();

        protected void Clear()
        {
            this._hand.Clear();
        }

        public bool TakeCard(Card card)
        {
            _hand.Add(card);
            return this.IsBusted();
        }

        public IEnumerable<int> CurrentValue()
        {
            int sum = Hand.Where(x => x.CardFace != CardFace.Ace).Sum(card => card.Value);
            int aceCount = Hand.Where(x => x.CardFace == CardFace.Ace).Count();

            if (aceCount == 0)
            {
                yield return sum;
                yield break;
            }

            if(aceCount == 1)
            {
                yield return sum + 1;
                if(sum + 11 > 21)
                {
                    yield break;
                }
                yield return sum + 11;
            }
            else
            {
                int add = aceCount - 1;
                if(sum + 11 + add <= 21)
                {
                    yield return sum + 11 + add;
                }
                yield return sum + aceCount;
            }




        }

        public Card SecondCard { get { return this._hand[1]; } }


        public bool IsBusted()
        {
            return this.CurrentValue().All(x => x > 21);
        }

        public IEnumerable<Card> Hand
        {
            get { return this._hand; }
        }

        public bool HasBlackJack()
        {
            return (Hand.Count() == 2 && Hand.Any(x => x.CardFace == CardFace.Ace) && Hand.Any(x => x.Value == 10));
        }


    }
}