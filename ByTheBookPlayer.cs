using System;
using System.Diagnostics;

namespace Blackjack
{
    [DebuggerDisplay("{Name} - {TheHand}")]
    public class ByTheBookPlayer : Player
    {
        private readonly PlayAction[,] _hardTable =
        {
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            }
        };

        private readonly PlayAction[,] _softTable =
        {
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            // Remove this once splitting is enabled
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.DoubleOrStay, PlayAction.DoubleOrStay, PlayAction.DoubleOrStay,
                PlayAction.DoubleOrStay, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit, PlayAction.Hit,
                PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            }
        };

        // Correct _splitTable
        private PlayAction[,] _splitTable =
        {
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Hit, PlayAction.Stay, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit, PlayAction.DoubleOrHit,
                PlayAction.DoubleOrHit, PlayAction.Stay, PlayAction.Stay, PlayAction.Hit, PlayAction.Hit
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            }
        };

        public ByTheBookPlayer(string name)
            : base(name)
        {
        }

        public override PlayAction Play(Card dealersTopCard)
        {
            var dealerCardValue = dealersTopCard.Value;
            var softValue = Hand.SoftValue;
            // TODO: complete split functionality
            //if (this.Hand.CanSplit)
            //{
            //    return _splitTable[,dealerCardValue - 1];
            //}
            PlayAction returnValue;
            if (null != softValue)
            {
                softValue -= 12;
                softValue = Math.Min(softValue.Value, 6);
                returnValue = _softTable[softValue.Value, dealerCardValue - 1];
            }
            else
            {
                var index = Hand.HardValue - 8;
                index = Math.Max(index, 0);
                index = Math.Min(index, 9);
                returnValue = _hardTable[index, dealerCardValue - 1];
            }
            if (returnValue == PlayAction.DoubleOrHit)
            {
                if (Hand.CanDouble)
                {
                    returnValue = PlayAction.Double;
                }
                else
                {
                    returnValue = PlayAction.Hit;
                }
            }
            else if (returnValue == PlayAction.DoubleOrStay)
            {
                if (Hand.CanDouble)
                {
                    returnValue = PlayAction.Double;
                }
                else
                {
                    returnValue = PlayAction.Stay;
                }
            }
            return returnValue;
        }
    }
}