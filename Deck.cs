using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class Deck : IEnumerable<Card>
    {
        private readonly Card[] _cards = new Card[52];

        public Deck()
        {
            for (var i = 0; i < 4; i++)
            {
                var suit = (Suit) i;
                for (var j = 1; j <= 13; j++)
                {
                    var cardFace = (CardFace) j;
                    var card = new Card(suit, cardFace);
                    var index = (13*i) + j - 1;
                    _cards[index] = card;
                }
            }
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return new List<Card>(_cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Shuffle(int times)
        {
            var cards = Enumerable.Range(0, 52);
            var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToArray();
            foreach (var index in cards)
            {
                Swap(index, shuffledcards[index]);
            }
        }

        private void Swap(int a, int b)
        {
            var tmp = _cards[a];
            _cards[a] = _cards[b];
            _cards[b] = tmp;
        }
    }
}