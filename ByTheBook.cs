using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Testing;

namespace ConsoleApplication7
{
    [DebuggerDisplay("{Name} - {TheHand}")]
    public class ByTheBook : Player
    {
        public ByTheBook(string name)
            : base(name)
        {
        }


        public override PlayAction Play(Card dealersTopCard)
        {
            if (this.Hand.Count() == 2)
                return this.PlayFirstHand(dealersTopCard);


            var dealerCardValue = dealersTopCard.Value;
            var softValue = this.Hand.SoftValue;
            if (null != softValue)
            {
                var soft = softValue.Value;
                if (soft == 13 && (dealerCardValue >= 2 && dealerCardValue <= 4))
                    return PlayAction.Hit;
                if (soft == 13 && (dealerCardValue >= 5 && dealerCardValue <= 6))
                    return PlayAction.Hit;
                if (soft == 13)
                    return PlayAction.Hit;
                if (soft == 14 && (dealerCardValue >= 2 && dealerCardValue <= 4))
                    return PlayAction.Hit;
                if (soft == 14 && (dealerCardValue >= 5 && dealerCardValue <= 6))
                    return PlayAction.Hit;
                if (soft == 14)
                    return PlayAction.Hit;
                if (soft == 15 && (dealerCardValue >= 2 && dealerCardValue <= 3))
                    return PlayAction.Hit;
                if (soft == 15 && (dealerCardValue >= 4 && dealerCardValue <= 6))
                    return PlayAction.Hit;
                if (soft == 15)
                    return PlayAction.Hit;
                if (soft == 16 && (dealerCardValue >= 2 && dealerCardValue <= 3))
                    return PlayAction.Hit;
                if (soft == 16 && (dealerCardValue >= 4 && dealerCardValue <= 6))
                    return PlayAction.Hit;
                if (soft == 16)
                    return PlayAction.Hit;
                if (soft == 17 && (dealerCardValue == 2))
                    return PlayAction.Hit;
                if (soft == 17 && (dealerCardValue >= 2 && dealerCardValue <= 6))
                    return PlayAction.Hit;
                if (soft == 17)
                    return PlayAction.Hit;

                if (soft == 18 && (dealerCardValue == 2))
                    return PlayAction.Stay;
                if (soft == 18 && (dealerCardValue >= 3 && dealerCardValue <= 6))
                    return PlayAction.Hit;
                if (soft == 18 && (dealerCardValue >= 7 && dealerCardValue <= 8))
                    return PlayAction.Stay;
                if (soft == 18)
                    return PlayAction.Hit;
                if (!(soft == 19 || soft == 20))
                    throw new Exception("error");
                return PlayAction.Stay;
            }

            var hardValue = this.Hand.HardValue;
            if (hardValue <= 8)
                return PlayAction.Hit;
            if (hardValue == 9 && dealerCardValue == 2)
                return PlayAction.Hit;
            if (hardValue == 9 && (dealerCardValue >= 3 && dealerCardValue >= 6))
                return PlayAction.Hit;
            if (hardValue == 9)
                return PlayAction.Hit;
            if (hardValue == 10 && (dealerCardValue >= 2 && dealerCardValue <= 9))
                return PlayAction.Hit;
            if (hardValue == 10)
                return PlayAction.Hit;
            if (hardValue == 11 && dealerCardValue != 1)
                return PlayAction.Hit;
            if (hardValue == 11)
                return PlayAction.Hit;
            if (hardValue == 12 && (dealerCardValue >= 2 && dealerCardValue <= 3))
                return PlayAction.Hit;
            if (hardValue == 12 && (dealerCardValue >= 4 && dealerCardValue <= 6))
                return PlayAction.Stay;
            if (hardValue == 12)
                return PlayAction.Hit;
            if ((hardValue >= 13 && hardValue <= 20) && (dealerCardValue >= 2 && dealerCardValue <= 6))
                return PlayAction.Stay;
            if (hardValue >= 13 && hardValue <= 16)
                return PlayAction.Hit;
            if (!(hardValue >= 17 && hardValue <= 21))
                throw new Exception("error");
            return PlayAction.Stay;


        }

        PlayAction PlayFirstHand(Card dealersTopCard)
        {
            var dealerCardValue = dealersTopCard.Value;
            var softValue = this.Hand.SoftValue;
            if (null != softValue)
            {
                var soft = softValue.Value;
                if (soft == 13 && (dealerCardValue >= 2 && dealerCardValue <= 4))
                    return PlayAction.Hit;
                if (soft == 13 && (dealerCardValue >= 5 && dealerCardValue <= 6))
                    return PlayAction.Double;
                if (soft == 13)
                    return PlayAction.Hit;
                if (soft == 14 && (dealerCardValue >= 2 && dealerCardValue <= 4))
                    return PlayAction.Hit;
                if (soft == 14 && (dealerCardValue >= 5 && dealerCardValue <= 6))
                    return PlayAction.Double;
                if (soft == 14)
                    return PlayAction.Hit;
                if (soft == 15 && (dealerCardValue >= 2 && dealerCardValue <= 3))
                    return PlayAction.Hit;
                if (soft == 15 && (dealerCardValue >= 4 && dealerCardValue <= 6))
                    return PlayAction.Double;
                if (soft == 15)
                    return PlayAction.Hit;
                if (soft == 16 && (dealerCardValue >= 2 && dealerCardValue <= 3))
                    return PlayAction.Hit;
                if (soft == 16 && (dealerCardValue >= 4 && dealerCardValue <= 6))
                    return PlayAction.Double;
                if (soft == 16)
                    return PlayAction.Hit;
                if (soft == 17 && (dealerCardValue == 2))
                    return PlayAction.Hit;
                if (soft == 17 && (dealerCardValue >= 2 && dealerCardValue <= 6))
                    return PlayAction.Double;
                if (soft == 17)
                    return PlayAction.Hit;

                if (soft == 18 && (dealerCardValue == 2))
                    return PlayAction.Stay;
                if (soft == 18 && (dealerCardValue >= 3 && dealerCardValue <= 6))
                    return PlayAction.Double;
                if (soft == 18 && (dealerCardValue >= 7 && dealerCardValue <= 8))
                    return PlayAction.Stay;
                if (soft == 18)
                    return PlayAction.Hit;
                if (!(soft == 19 || soft == 20))
                    throw new Exception("error");
                return PlayAction.Stay;
            }

            var hardValue = this.Hand.HardValue;
            if (hardValue <= 8)
                return PlayAction.Hit;
            if (hardValue == 9 && dealerCardValue == 2)
                return PlayAction.Hit;
            if (hardValue == 9 && (dealerCardValue >= 3 && dealerCardValue >= 6))
                return PlayAction.Double;
            if (hardValue == 9)
                return PlayAction.Hit;
            if (hardValue == 10 && (dealerCardValue >= 2 && dealerCardValue <= 9))
                return PlayAction.Hit;
            if (hardValue == 10)
                return PlayAction.Hit;
            if (hardValue == 11 && dealerCardValue != 1)
                return PlayAction.Double;
            if (hardValue == 11)
                return PlayAction.Hit;
            if (hardValue == 12 && (dealerCardValue >= 2 && dealerCardValue <= 3))
                return PlayAction.Hit;
            if (hardValue == 12 && (dealerCardValue >= 4 && dealerCardValue <= 6))
                return PlayAction.Stay;
            if (hardValue == 12)
                return PlayAction.Hit;
            if ((hardValue >= 13 && hardValue <= 20) && (dealerCardValue >= 2 && dealerCardValue <= 6))
                return PlayAction.Stay;
            if (hardValue >= 13 && hardValue <= 16)
                return PlayAction.Hit;
            if (!(hardValue >= 17 && hardValue <= 21))
                throw new Exception("error");
            return PlayAction.Hit;

        }
    }
}