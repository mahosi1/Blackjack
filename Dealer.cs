using System.Linq;

namespace Blackjack
{
    public class Dealer : Player
    {
        private Shoe _shoe;

        public Dealer(string name) : base(name)
        {
            Shuffle();
        }

        public Card TopCard
        {
            get { return Hand.First(); }
        }

        public Card Deal()
        {
            return _shoe.GetNextCard();
        }

        public override string ToString()
        {
            return string.Format("{0} Busts {1}", Name, Losses);
        }

        public override PlayAction Play(Card dealersTopCard)
        {
            if (null != Hand.SoftValue)
            {
                if (Hand.SoftValue.Value < 17)
                    return PlayAction.Hit;
            }
            if (Hand.HardValue < 17)
            {
                return PlayAction.Hit;
            }
            return PlayAction.Stay;
        }

        public bool NeedsToNewShoe()
        {
            return (_shoe.Remaining/_shoe.Capacity) < .2;
        }

        public void Shuffle()
        {
            _shoe = Shoe.Create(7);
        }
    }
}