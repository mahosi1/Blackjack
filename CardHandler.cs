using System.Text;

namespace Blackjack
{
    public abstract class CardHandler
    {
        public Hand Hand { get; private set; } = new Hand();

        public string ToStringOfHand()
        {
            var sb = new StringBuilder();
            foreach (var card in Hand)
            {
                sb.AppendFormat("{0}-{1}\n", card.CardFace, card.Suit);
            }
            return sb.ToString();
        }

        protected void Clear()
        {
            Hand = new Hand();
        }

        public bool TakeCard(Card card)
        {
            Hand.Add(card);
            return Hand.IsBusted;
        }
    }
}