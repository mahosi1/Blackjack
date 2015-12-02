using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    public class Table
    {
        public void PlayHand(Shoe shoe, Player[] players, Player dealer)
        {
            Card dealersTopCard = SeedTable(players, dealer, shoe);

            bool outstanding = false;

            if (!dealer.Hand.IsBlackjack)
            {
                foreach (Player player in players)
                {
                    if (player.Hand.IsBlackjack)
                        continue;
                    outstanding |= PlayHand(player, dealersTopCard, shoe);
                }
                if (outstanding)
                    PlayHand(dealer, dealersTopCard, shoe);
            }
            Payout(players, dealer);
        }
        Card SeedTable(Player[] players, Player dealer, Shoe shoe)
        {
            players.ForEach(x => x.TakeCard(shoe.GetNextCard()));
            dealer.TakeCard(shoe.GetNextCard());
            players.ForEach(x => x.TakeCard(shoe.GetNextCard()));

            Card dealerTopCard = shoe.GetNextCard();
            dealer.TakeCard(dealerTopCard);

            return dealerTopCard;
        }

        ILogger logger = new NullLogger();

        void Payout(Player[] players, Player dealer)
        {
            
            var tmp = new List<Player>(players);

            var sb = new StringBuilder();
            foreach (var card in dealer.Hand)
            {
                sb.AppendFormat("{0} - {1}\n", card.CardFace, card.Suit);
            }
            logger.WriteLine("\n\nDEALER ({1}) \n{0} ", sb, dealer.Hand.Final);

            foreach (var player in players)
            {
                logger.WriteLine("Player: {0}({2}) \n{1} ", player.Name,
                    player.ToStringOfHand(), player.Hand.Final);
            }
            if (dealer.Hand.IsBlackjack)
            {
                logger.WriteLine("dealer got blackjack");
                var ties = tmp.Where(x => x.Hand.IsBlackjack).ToArray();
                foreach (var player in ties)
                {
                    logger.WriteLine("loss from dealer bj");
                    player.Payout(0);
                    tmp.Remove(player);
                }
                var losers = tmp.Except(ties);

                losers.ForEach(x => x.Payout(-1));
                dealer.Payout(1);
                return;
            }

            var busted = tmp.Where(x => x.Hand.IsBusted).ToArray();
            if (busted.Any())
            {
                foreach (var player in busted)
                {
                    player.Payout(-1);
                    tmp.Remove(player);
                }
            }

            if (dealer.Hand.IsBusted)
            {
                logger.WriteLine("Dealer busted");
                var winners = tmp.Where(x => !x.Hand.IsBusted).ToArray();
                foreach (var player in winners)
                {
                    player.Payout(1);
                    tmp.Remove(player);
                }

                dealer.Payout(-1);
            }
            else
            {
                var winners = tmp.Where(x => x.Hand.Final > dealer.Hand.Final && !x.Hand.IsBusted).ToArray();

                foreach (var player in winners)
                {
                    player.Payout(1);
                    tmp.Remove(player);
                }
                var ties = tmp.Where(x => x.Hand.Final == dealer.Hand.Final && !x.Hand.IsBusted).ToArray();
                foreach (var player in ties)
                {
                    player.Payout(0);
                    tmp.Remove(player);
                }
                var losers = tmp.Where(x => !x.Hand.IsBusted);
                losers.ForEach(x => x.Payout(-1));
            }
            dealer.Reset();
        }
        bool PlayHand(Player player, Card dealerTopCard, Shoe shoe)
        {
            PlayAction play = player.Play(player.Hand, dealerTopCard);
            if (play == PlayAction.Stay)
            {
                return true;
            }
            if (play == PlayAction.Hit)
            {
                Card card = shoe.GetNextCard();
                bool bust = player.TakeCard(card);
                if (bust)
                    return false;
                return PlayHand(player, dealerTopCard, shoe);
            }
            if (play == PlayAction.Double)
            {
                if (player.Hand.Count() != 2)
                    throw new Exception("only double on first");
                var bust = player.TakeCard(shoe.GetNextCard());
                if (bust)
                    return false;
                return true;
            }
            throw new InvalidOperationException("shouldn't be here");
        }
    }
}