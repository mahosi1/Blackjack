using System;
using System.Collections.Generic;

namespace Blackjack
{
    public abstract class Player : CardHandler, IEquatable<Player>
    {
        private readonly List<bool?> _results = new List<bool?>();
        private int _pushes;
        private int _wins;

        protected Player(string name)
        {
            Name = name;
        }

        public string TheHand
        {
            get { return ToStringOfHand(); }
        }

        public string Name { get; }
        public int Losses { get; private set; }

        public bool Equals(Player other)
        {
            return this == other;
        }

        public event Action<Player> Quitted;

        public void Reset()
        {
            Clear();
        }

        public abstract PlayAction Play(Card dealersTopCard);

        public void Payout(int payout)
        {
            if (payout > 0)
            {
                _results.Add(true);
            }
            else if (payout < 0)
            {
                _results.Add(false);
            }
            else
            {
                _results.Add(null);
            }
            Utility.WriteLine("Payout of {0} to {1} with {2}", payout, Name, Hand.Final);
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
                Losses++;
            }
            Reset();
        }

        protected void Quit()
        {
            Quitted(this);
        }

        public override string ToString()
        {
            return string.Format("{0} Wins({1}), Ties({2}), Losses({3}), {4}% win ", Name, _wins, _pushes, Losses,
                (((double) _wins)/(_wins + Losses)).ToString("P"));
        }
    }
}