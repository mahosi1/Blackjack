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

        public void SetCard(Suit suit, CardFace cardFace, int index)
        {
            this.AtIndex(suit, cardFace, index);
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


            if (this.CurrentValue().Any(x => x >= 18))
            {
                return PlayAction.Stay;
            }



            if(this.CurrentValue().Any(x => x == 17) && !this.SoftValues().Any())
            {
                //if (this.Hand.Any(x => x.CardFace == CardFace.Ace))
                //{
                //    //Debugger.Break();
                //}

                return PlayAction.Stay;
            }
            //Console.Out.WriteLine("Hitting on a ");
            //this.CurrentValue().ForEach(x => Console.Out.WriteLine("\t{0}", x));


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