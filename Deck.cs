using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class Deck : IEnumerable<Card>
    {
        readonly Card[] _cards = new Card[52];

        public Deck()
        {
            for (var i = 0; i < 4; i++)
            {
                var suit = (Suit)i;
                for (var j = 1; j <= 13; j++)
                {
                    var cardFace = (CardFace)j;
                    var card = new Card(suit, cardFace);
                    var index = (13 * i) + j - 1;
                    _cards[index] = card;
                }
            }

            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToArray();
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return new List<Card>(_cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}