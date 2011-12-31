using System.Diagnostics;

namespace ConsoleApplication7
{
    [DebuggerDisplay("{CardFace} - {Suit}")]
    public class Card
    {
        private readonly Suit _suit;
        private readonly CardFace _cardFace;

        public Card(Suit suit, CardFace cardFace)
        {
            _suit = suit;
            _cardFace = cardFace;
        }

        public CardFace CardFace { get { return this._cardFace; } }

        public Suit Suit
        {
            get { return _suit; }
        }

        public int Value
        {
            get { return (int) _cardFace >= 10 ? 10 : (int) _cardFace; }
        }

    }
}