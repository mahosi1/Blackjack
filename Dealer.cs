using System.Linq;
using ConsoleApplication7;

namespace Testing
{
    public class Dealer : Player
    {
        private Shoe _shoe;

        public Dealer(string name) :base(name)
        {
            this.Shuffle();
        }

        public Card TopCard
        {
            get { return this.Hand.First(); }
        }


        public Card Deal()
        {
            return this._shoe.GetNextCard();
        }

        public override string ToString()
        {
            return string.Format("{0} Busts {1}", this.Name, this.Losses);
        }


        public override PlayAction Play(Card dealersTopCard)
        {
            if(null != this.Hand.SoftValue)
            {
                if (this.Hand.SoftValue.Value < 17)
                    return PlayAction.Hit;
            }
            if (this.Hand.HardValue < 17)
                return PlayAction.Hit;
            return PlayAction.Stay;
        }

        public bool NeedsToNewShoe()
        {
            return (_shoe.Remaining / _shoe.Capacity) < .2;
        }

        public void Shuffle()
        {
            _shoe = Shoe.Create(7);
        }

    }
}