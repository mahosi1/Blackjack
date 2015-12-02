using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class Hand : IEnumerable<Card>
    {
        private readonly List<Card> _cards = new List<Card>();

        public bool CanDouble
        {
            get { return _cards.Count == 2; }
        }

        public bool IsBusted
        {
            get { return null == SoftValue && HardValue > 21; }
        }

        public bool IsBlackjack
        {
            get { return _cards.Count == 2 && SoftValue.GetValueOrDefault() == 21; }
        }

        public bool CanSplit
        {
            get { return _cards.Count == 2 && _cards[0] == _cards[1]; }
        }

        public int Final
        {
            get
            {
                return SoftValue.GetValueOrDefault() > HardValue
                    ? SoftValue.GetValueOrDefault()
                    : HardValue;
            }
        }

        public int? SoftValue
        {
            get
            {
                var sum = _cards.Sum(x => x.Value);
                if (_cards.Any(x => x.CardFace == CardFace.Ace))
                {
                    sum += 10;
                    if (sum <= 21) return sum;
                }
                return null;
            }
        }

        public int HardValue
        {
            get { return _cards.Sum(x => x.Value); }
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }
    }
}