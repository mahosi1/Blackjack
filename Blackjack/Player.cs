using System.Text;

namespace Blackjack
{
    public class Player
    {
        private int _pushes;
        private int _wins;
        private int _losses;
        private readonly IPlayerStrategy _strategy;

        public Hand Hand { get; private set; } = new Hand();

        public Player(string name, IPlayerStrategy strategy)
        {
            Name = name;
            _strategy = strategy;
        }

        public string Name { get; }

        public void Reset()
        {
            Hand = new Hand();
        }

        public PlayAction Play(Hand hand, Card dealersTopCard)
        {
            return _strategy.Play(hand, dealersTopCard);
        }

        public void Payout(int payout)
        {
                        if (payout == 0)
            {
                _pushes++;
            }
            else if (payout > 0)
            {
                _wins++;
            }
            else
            {
                _losses++;
            }
            Reset();
        }

        public override string ToString()
        {
            return string.Format("{0} Wins({1}), Ties({2}), _losses({3}), {4}% win ", Name, _wins, _pushes, _losses,
                (((double) _wins)/(_wins + _losses)).ToString("P"));
        }

        public string ToStringOfHand()
        {
            var sb = new StringBuilder();
            foreach (var card in Hand)
            {
                sb.AppendFormat("{0}-{1}\n", card.CardFace, card.Suit);
            }
            return sb.ToString();
        }

        public bool TakeCard(Card card)
        {
            Hand.Add(card);
            return Hand.IsBusted;
        }
    }
}