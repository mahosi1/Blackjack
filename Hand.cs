using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7;

namespace Testing
{
    public class Hand : IEnumerable<Card>
    {
        readonly List<Card> _cards = new List<Card>();

        public void Add(Card card)
        {
            this._cards.Add(card);
        }

        public bool CanDouble
        {
            get { return this._cards.Count == 2; }
        }

        public bool IsBusted
        {
            get { return null == this.SoftValue && this.HardValue > 21; }
        }

        public bool IsBlackjack
        {
            get { return this._cards.Count == 2 && this.SoftValue.GetValueOrDefault() == 21; }
        }

        public int Final
        {
            get
            {
                return this.SoftValue.GetValueOrDefault() > this.HardValue
                           ? this.SoftValue.GetValueOrDefault()
                           : this.HardValue;
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
            get
            {
                var sum = _cards.Sum(x => x.Value);
                return sum;
            }
        }


        public IEnumerator<Card> GetEnumerator()
        {
            return this._cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}