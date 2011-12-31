using System;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication7
{

    public abstract class Player : CardHandler, IEquatable<Player>
    {
        private readonly string _name;
        public event Action<Player> Quitted;

        public string TheHand
        {
            get { return this.ToStringOfHand(); }
        }

        protected Player(string name)
        {
            _name = name;
        }

        public abstract PlayAction Play(Card dealersTopCard);

        int _wins;
        int _losses;
        int _pushes;

        public void Payout(int payout)
        {

            Utility.WriteLine("Payout of {0} to {1} with {2}", payout, this.Name, 
            this.ToStringOfHand());
            if (_hand.Count == 0)
                Debugger.Break();
            if (payout == 0)
            {
                //Console.Out.WriteLine("TIED");
                _pushes++;
            }
            else if (payout > 0)
            {
                //Console.Out.WriteLine("WON");
                _wins++;
            }
            else
            {
                //Console.Out.WriteLine("LOST");
                _losses++;
            }
            this.Clear();
        }

        public string Name { get { return this._name; } }

        public int Losses { get { return this._losses; } }


        protected void Quit()
        {
            Quitted(this);
        }

        public bool Equals(Player other)
        {
            return this == other;
        }

        public override string ToString()
        {
            return string.Format("{0} Wins({1}), Ties({2}), Losses({3}), ", this._name, this._wins, this._pushes, this._losses);
        }

    }
}