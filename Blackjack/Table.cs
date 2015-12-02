using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    public class Table
    {
        private readonly Player _dealer = new Player("Dealer", new DealerStrategy());
        private readonly Player[] _players;
        private Shoe _shoe = Shoe.Create(7);
        //private ILogger _logger = new TraceLogger();
        private ILogger _logger = new NullLogger();

        public Table(int seats, int minimumBet)
        {
            Seats = seats;
            MinimumBet = minimumBet;
            _players = new Player[seats];
        }
        public int Seats { get; }
        public int MinimumBet { get; }

        public void AddPlayer(Player player, int seat)
        {
            if (_players[seat] != null)
            {
                throw new Exception("can't sit on top of another player");
            }
            _players[seat] = player;
        }

        public void AddPlayer(string name, IPlayerStrategy strategy)
        {
            var index = Array.FindIndex(_players, x => x == null);
            AddPlayer(new Player(name, strategy), index);
        }

        public Card Deal()
        {
            return _shoe.GetNextCard();
        }

        public void PlayHand()
        {
            if (_shoe.NeedsNewShoe())
            {
                _shoe = Shoe.Create(7);
            }
            var toPlay = _players.Where(x => null != x).ToArray();
            foreach (var player in toPlay)
            {
                player.TakeCard(Deal());
            }
            _dealer.TakeCard(Deal());

            foreach (var player in toPlay)
            {
                player.TakeCard(Deal());
            }
            _dealer.TakeCard(Deal());

            if (!_dealer.Hand.IsBlackjack)
            {
                foreach (var player in toPlay)
                {
                    if (player.Hand.IsBlackjack)
                    {
                        continue;
                    }
                    PlayHand(player);
                }
                PlayHand(_dealer);
            }
            Payout(toPlay, _dealer);
        }

        private void Payout(Player[] players, Player dealer)
        {
            var tmp = new List<Player>(players);

            var sb = new StringBuilder();
            foreach (var card in dealer.Hand)
            {
                sb.AppendFormat("{0} - {1}\n", card.CardFace, card.Suit);
            }
            _logger.WriteLine("\n\nDEALER ({1}) \n{0} ", sb, dealer.Hand.Final);

            foreach (var player in players)
            {
                _logger.WriteLine("Player: {0}({2}) \n{1} ", player.Name,
                    player.ToStringOfHand(), player.Hand.Final);
            }
            if (dealer.Hand.IsBlackjack)
            {
                _logger.WriteLine("dealer got blackjack");
                var ties = tmp.Where(x => x.Hand.IsBlackjack).ToArray();
                foreach (var player in ties)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", 0, player.Name, player.Hand.Final);
                    player.Payout(0);
                    tmp.Remove(player);
                }
                var losers = tmp.Except(ties);

                foreach (var player in losers)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", -1, player.Name, player.Hand.Final);
                    player.Payout(-1);
                }
                dealer.Payout(1);
                return;
            }

            var busted = tmp.Where(x => x.Hand.IsBusted).ToArray();
            if (busted.Any())
            {
                foreach (var player in busted)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", -1, player.Name, player.Hand.Final);
                    player.Payout(-1);
                    tmp.Remove(player);
                }
            }

            if (dealer.Hand.IsBusted)
            {
                _logger.WriteLine("Dealer busted");
                var winners = tmp.Where(x => !x.Hand.IsBusted).ToArray();
                foreach (var player in winners)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", 1, player.Name, player.Hand.Final);
                    player.Payout(1);
                    tmp.Remove(player);
                }
                _logger.WriteLine("Payout of {0} to {1} with {2}", -1, dealer.Name, dealer.Hand.Final);
                dealer.Payout(-1);
            }
            else
            {
                var winners = tmp.Where(x => x.Hand.Final > dealer.Hand.Final && !x.Hand.IsBusted).ToArray();

                foreach (var player in winners)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", 1, player.Name, player.Hand.Final);
                    player.Payout(1);
                    tmp.Remove(player);
                }
                var ties = tmp.Where(x => x.Hand.Final == dealer.Hand.Final && !x.Hand.IsBusted).ToArray();
                foreach (var player in ties)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", 0, player.Name, player.Hand.Final);
                    player.Payout(0);
                    tmp.Remove(player);
                }
                var losers = tmp.Where(x => !x.Hand.IsBusted);
                foreach (var player in losers)
                {
                    _logger.WriteLine("Payout of {0} to {1} with {2}", -1, player.Name, player.Hand.Final);
                    player.Payout(-1);
                }
            }
            _dealer.Reset();
        }

        private void PlayHand(Player player)
        {
            var play = player.Play(player.Hand, _dealer.Hand.First());
            if (play == PlayAction.Stay)
            {
                return;
            }
            if (play == PlayAction.Hit)
            {
                var c = Deal();
                var bust = player.TakeCard(c);
                if (!bust)
                {
                    PlayHand(player);
                }
            }
            else if (play == PlayAction.Double)
            {
                if (player.Hand.Count() != 2)
                {
                    throw new Exception("only double on first");
                }
                player.TakeCard(Deal());
            }
        }

        public void ReportStats()
        {
            foreach (var player in _players.Where(x => null != x).ToArray())
            {
                Console.Out.WriteLine(player.ToString());
            }
            Console.Out.WriteLine(_dealer.ToString());
        }
    }
}