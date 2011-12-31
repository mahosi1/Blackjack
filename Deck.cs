using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication7
{
    public class Deck : IEnumerable<Card>
    {
        readonly Card[] _cards = new Card[52];

        public Deck()
        {
            for (int i = 0; i < 4; i++)
            {
                var suit = (Suit) i;
                for (int j = 1; j <= 13; j++)
                {
                    var cardFace = (CardFace) j;
                    var card = new Card(suit, cardFace);
                    int index = (13*i) + j-1;
                    this._cards[index] = card;
                }
            }
        }

        public void Shuffle(int times)
        {
            this.Shuffle2();
            for (int i = 0; i < times; i++)
            {
                this.Shuffle();
            }
        }

        void Shuffle()
        {
            var r = new Random();
            for(int i = 0; i < _cards.Length; i++)
            {
                int index = r.Next(0, 51);
                this.Swap(i, index);
                r = new Random(r.Next());
            }
        }

        void Swap(int a, int b)
        {
            var tmp = _cards[a];
            _cards[a] = _cards[b];
            _cards[b] = tmp;
        }

        void Shuffle2()
        {
            IEnumerable<int> cards = Enumerable.Range(0, 52);
            var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToArray();
            foreach (int index in cards)
            {
                this.Swap(index, shuffledcards[index]);
            }

            
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return new List<Card>(this._cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}