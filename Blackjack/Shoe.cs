using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Shoe
    {
        private readonly int _capactiy;
        private readonly List<Card> _cards = new List<Card>();
        private int? _cut;

        public Shoe(int decks)
        {
            for (var i = 0; i < decks; i++)
            {
                var deck = new Deck();
                foreach (var card in deck)
                {
                    _cards.Add(card);
                }
            }
            _capactiy = _cards.Count;
            this.Cut();
        }

        public double Remaining
        {
            get { return _cards.Count; }
        }

        public double Capacity
        {
            get { return _capactiy; }
        }

        void Cut()
        {
            if (_cut != null)
            {
                throw new Exception("Cant cut twice");
            }
            var percent = Math.Max(new Random().Next(20, 80) / 100d, new Random().NextDouble());
            _cut = (int)(percent * _capactiy);
        }

        public Card GetNextCard()
        {
            var card = _cards[0];
            _cards.Remove(card);
            return card;
        }

        public bool NeedsNewShoe()
        {
            return _cut >= _cards.Count;
        }
    }
}