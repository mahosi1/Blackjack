using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication7;

namespace Testing
{
    public abstract class CardHandler
    {
        public int FinalAmount
        {
            get
            {
                int final = 0;
                foreach (int val in this.CurrentValue().OrderByDescending(x => x))
                {
                    if (final == 0)
                        final = val;
                    else if (val > final && val <= 21)
                        final = val;
                }
                return final;
                //IOrderedEnumerable<int> found = from p in this.CurrentValue()
                //            orderby p descending
                //            select p;


                //if (found.Count() >= 1)
                //    return found.First();

                //return this.CurrentValue().First();
            }
        }


        protected void AtIndex(Suit suit, CardFace cardFace, int index)
        {
            this._hand[index] = new Card(suit, cardFace);
        }


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

        public IEnumerable<int>  SoftValues()
        {
            int sum = Hand.Where(x => x.CardFace != CardFace.Ace).Sum(card => card.Value);
            int aceCount = Hand.Where(x => x.CardFace == CardFace.Ace).Count();

            if (aceCount == 0)
            {
                yield break;
            }

            if (aceCount == 1)
            {
                if (sum + 11 > 21)
                {
                    yield break;
                }
                yield return sum + 11;
            }
            else
            {
                int add = aceCount - 1;
                if (sum + 11 + add <= 21)
                {
                    yield return sum + 11 + add;
                }
                yield break;
            }
            
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

        public Card FirstCard { get { return this._hand[0]; } }


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