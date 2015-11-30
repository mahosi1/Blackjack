using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Shoe
    {
        private readonly int _capactiy;
        private readonly List<Card> _cards = new List<Card>();
        private readonly int _deckCount;
        private int? _cut;

        private Shoe(int decks)
        {
            _deckCount = decks;

            for (var i = 0; i < decks; i++)
            {
                var deck = new Deck();
                deck.Shuffle(100);
                foreach (var card in deck)
                {
                    _cards.Add(card);
                }
            }
            _capactiy = _cards.Count;
        }

        public double Remaining
        {
            get { return _cards.Count; }
        }

        public double Capacity
        {
            get { return _capactiy; }
        }

        public static Shoe Create(int decks)
        {
            return new Shoe(decks);
        }

        public void Cut(int percent)
        {
            if (_cut != null)
            {
                throw new Exception("Cant cut twice");
            }

            var position = (_deckCount*52)*percent;
            if (position < 52 || position > 90000)
            {
                throw new InvalidCutException();
            }
            _cut = position;
        }

        public Card GetNextCard()
        {
            var card = _cards[0];
            _cards.Remove(card);
            return card;
        }
    }
}