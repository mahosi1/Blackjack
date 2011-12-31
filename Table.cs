using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApplication7
{
    public class Table
    {
        private readonly int _seats;
        private readonly int _minimumBet;
        private readonly Dealer _dealer = new Dealer("Freddie");
        private readonly Player[] _players;

        public Table(int seats, int minimumBet)
        {
            _seats = seats;
            _minimumBet = minimumBet;
            _players = new Player[seats];
        }

        public int Seats
        {
            get { return _seats; }
        }

        public int MinimumBet
        {
            get { return _minimumBet; }
        }

        public void AddPlayer(Player player, int seat)
        {
            if(_players[seat] != null)
                throw new Exception("cant sit on top of another player");
            player.Quitted += PlayerQuitted;
            _players[seat] = player;
        }

        void PlayerQuitted(Player obj)
        {
            var index = Array.FindIndex(_players, x => x == obj);
            _players[index] = null;
        }

        public void AddPlayer(Player player)
        {
            var index = Array.FindIndex(_players, x => x == null);
            this.AddPlayer(player, index);
        }


        public void PlayHand()
        {
            if(_dealer.NeedsToNewShoe())
            {
                _dealer.Shuffle();
            }


            Player[] toPlay = _players.Where(x => null != x).ToArray();
            foreach (Player player in toPlay)
            {
                player.TakeCard(_dealer.Deal());
            }
            _dealer.TakeCard(_dealer.Deal());

            foreach (Player player in toPlay)
            {
                player.TakeCard(_dealer.Deal());
            }
            _dealer.TakeCard(_dealer.Deal());

            if(_dealer.HasBlackJack())
            {
                this.Payout(toPlay, _dealer);
                return;
            }


            foreach (Player player in toPlay)
            {
                if (player.HasBlackJack())
                    continue;
                this.PlayHand(player);
            }

            this.PlayHand(this._dealer);

            this.Payout(toPlay, this._dealer);

        }

        private void Payout(Player[] players, Dealer dealer)
        {

            var tmp = new List<Player>(players);

            var sb = new StringBuilder();
            foreach (Card card in dealer.Hand)
            {
                sb.AppendFormat("{0} - {1}\n", card.CardFace, card.Suit);
            }

            Utility.WriteLine("DEALER {1} = {0} ", sb, dealer.FinalAmount);

            Utility.WriteLine(" {0} - {1} ", players[0].Name,
                players[0].ToStringOfHand());


            
            if(dealer.HasBlackJack())
            {
                Utility.WriteLine("dealer got blackjack");
                var ties = tmp.Where(x => x.HasBlackJack()).ToArray();
                foreach (Player player in ties)
                {
                    Utility.WriteLine("loss from dealer bj");
                    player.Payout(0);
                    tmp.Remove(player);
                }
                var losers = tmp.Except(ties);
                
                losers.ForEach(x => x.Payout(-1));
                dealer.Payout(1);
                return;
            }

            var busted = tmp.Where(x => x.IsBusted()).ToArray();
            if(busted.Count() > 0)
            {
                foreach (Player player in busted)
                {
                    Utility.WriteLine("loss from busted ");
                    player.Payout(-1);
                    tmp.Remove(player);
                }

            }
            


            if (dealer.IsBusted())
            {
                Utility.WriteLine("Dealer busted");
                var winners = tmp.Where(x => !x.IsBusted()).ToArray();
                foreach (Player player in winners)
                {
                    player.Payout(1);
                    tmp.Remove(player);
                }
                
                dealer.Payout(-1);
            }
            else
            {

                
                var winners = tmp.Where(x => x.CurrentValue().Any(y => y > dealer.FinalAmount && y <= 21)).ToArray();
                
                foreach (Player player in winners)
                {
                    Utility.WriteLine("we got a win");
                    player.Payout(1);
                    tmp.Remove(player);

                }


                var ties = tmp.Where(x => x.CurrentValue().Any(y => y == dealer.FinalAmount)).ToArray();
                
                

                foreach (Player player in ties)
                {
                    Utility.WriteLine("we got a tie");
                    
                    player.Payout(0);
                    tmp.Remove(player);

                }


                var losers = tmp.Where(x => !x.IsBusted());
                Utility.WriteLine("loss from less than");
                losers.ForEach(x => x.Payout(-1));

            }




        }

        private void PlayHand(Player player)
        {
            PlayAction play = player.Play(this._dealer.SecondCard);
            if (play == PlayAction.Stay)
                return;
            if(play == PlayAction.Hit)
            {
                var bust = player.TakeCard(_dealer.Deal());
                if(!bust)
                    this.PlayHand(player);
            }

        }

        public void ReportStats()
        {
            _players.Where(x => null != x).ToArray().ForEach(x => Console.Out.WriteLine(x.ToString()));
            Console.Out.WriteLine(_dealer.ToString());
            
        }
    }
}