using System;
using System.Collections.Generic;

namespace ConsoleApplication7
{
    public class Shoe
    {
        private readonly List<Card> _cards =  new List<Card>();
        private readonly int _capactiy;
        private int? _cut;
        private readonly int _deckCount;

        public double Remaining
        {
            get { return _cards.Count; }
        }

        public double Capacity { get { return this._capactiy; } }


        public static Shoe Create(int decks)
        {
            return new Shoe(decks);
        }

        private Shoe(int decks)
        {
            _deckCount = decks;
            
            for (int i = 0; i < decks; i++)
            {
                var deck = new Deck();
                deck.Shuffle(100);
                foreach (Card card in deck)
                {
                    _cards.Add(card);
                }
            }
            _capactiy = _cards.Count;
        }


        public void Cut(int percent)
        {
            if (_cut != null)
                throw new Exception("Cant cut twice");

            var position = (_deckCount*52)*percent;
            if (position < 52 || position > 90000)
                throw new InvalidCutException();

            _cut = position;


            //this.Swap();


        }


        public Card GetNextCard()
        {

            Card card = this._cards[0];
            this._cards.Remove(card);
            return card;
        }



    }
}