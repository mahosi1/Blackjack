using System.Diagnostics;

namespace Blackjack
{
    [DebuggerDisplay("{CardFace} - {Suit}")]
    public class Card
    {
        public Card(Suit suit, CardFace cardFace)
        {
            Suit = suit;
            CardFace = cardFace;
        }

        public CardFace CardFace { get; }
        public Suit Suit { get; }

        public int Value
        {
            get { return (int) CardFace >= 10 ? 10 : (int) CardFace; }
        }
    }
}