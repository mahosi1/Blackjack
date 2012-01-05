using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication7;

namespace Testing
{
    public abstract class CardHandler
    {


        public string ToStringOfHand()
        {
            var sb = new StringBuilder();
            foreach (Card card in _hand)
            {
                sb.AppendFormat("{0}-{1}\n", card.CardFace, card.Suit);
            }
            return sb.ToString();
        }

        private Hand _hand = new Hand();

        protected void Clear()
        {
            this._hand = new Hand();
        }

        public bool TakeCard(Card card)
        {
            _hand.Add(card);
            return _hand.IsBusted;
        }


        public Hand Hand { get { return this._hand; } }

        //public IEnumerable<int> SoftValues()
        //{
        //    int sum = Hand.Where(x => x.CardFace != CardFace.Ace).Sum(card => card.Value);
        //    int aceCount = Hand.Where(x => x.CardFace == CardFace.Ace).Count();

        //    if (aceCount == 0)
        //    {
        //        yield break;
        //    }

        //    if (aceCount == 1)
        //    {
        //        if (sum + 11 > 21)
        //        {
        //            yield break;
        //        }
        //        yield return sum + 11;
        //    }
        //    else
        //    {
        //        int add = aceCount - 1;
        //        if (sum + 11 + add <= 21)
        //        {
        //            yield return sum + 11 + add;
        //        }
        //        yield break;
        //    }
            
        //}

        //public IEnumerable<int> CurrentValue()
        //{
        //    int sum = Hand.Where(x => x.CardFace != CardFace.Ace).Sum(card => card.Value);
        //    int aceCount = Hand.Where(x => x.CardFace == CardFace.Ace).Count();

        //    if (aceCount == 0)
        //    {
        //        yield return sum;
        //        yield break;
        //    }

        //    if(aceCount == 1)
        //    {
        //        yield return sum + 1;
        //        if(sum + 11 > 21)
        //        {
        //            yield break;
        //        }
        //        yield return sum + 11;
        //    }
        //    else
        //    {
        //        int add = aceCount - 1;
        //        if(sum + 11 + add <= 21)
        //        {
        //            yield return sum + 11 + add;
        //        }
        //        yield return sum + aceCount;
        //    }




        //}

        //public Card FirstCard { get { return this._hand[0]; } }




    }
}