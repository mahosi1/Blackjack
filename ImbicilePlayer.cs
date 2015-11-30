using System;
using System.Diagnostics;

namespace Blackjack
{
    [DebuggerDisplay("{Name} - {TheHand}")]
    public class ImbicilePlayer : Player
    {
        private readonly PlayAction[,] _hardTable =
        {
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            }
        };

        private readonly PlayAction[,] _softTable =
        {
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            // Remove this once splitting is enabled
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            }
        };

        // Correct _splitTable
        private PlayAction[,] _splitTable =
        {
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Stay, PlayAction.Hit, PlayAction.Stay, PlayAction.Stay, PlayAction.Stay,
                PlayAction.Stay, PlayAction.Hit, PlayAction.Hit, PlayAction.Stay, PlayAction.Stay
            },
            {
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit,
                PlayAction.Hit, PlayAction.Hit, PlayAction.Hit, PlayAction.Hit
            }
        };

        public ImbicilePlayer(string name)
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