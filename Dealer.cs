using System.Linq;

namespace ConsoleApplication7
{
    public class Dealer : Player
    {
        private Shoe _shoe;

        public Dealer(string name) :base(name)
        {
            this.Shuffle();
        }

        public int FinalAmount
        {
            get
            {
                IOrderedEnumerable<int> found = from p in this.CurrentValue()
                            where p < 22
                            orderby p descending
                            select p;
                if (found.Count() >= 1)
                    return found.First();

                return this.CurrentValue().First();



            }
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
            if (this.CurrentValue().Any(x => x >= 17))
            {
                return PlayAction.Stay;
            }
            return PlayAction.Hit;
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