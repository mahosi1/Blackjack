using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ConsoleApplication7;

namespace Testing
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

            if (!_dealer.Hand.IsBlackjack)
            {
                foreach (Player player in toPlay)
                {
                    if (player.Hand.IsBlackjack)
                        continue;
                    this.PlayHand(player);
                }

                this.PlayHand(this._dealer);
            }

            //this.TraceOutPlayerback(toPlay, this._dealer);

            this.Payout(toPlay, this._dealer);

        }

        //private void TraceOutPlayerback(Player[] toPlay, Dealer dealer)
        //{
        //    return;
        //    foreach (Player player in toPlay)
        //    {

        //        Utility.WriteLine("{0}\n{1}\n{2}\n\n", player.Name,
        //            player.ToStringOfHand(), player.CurrentValue().First());
        //    }

        //    Utility.WriteLine("{0}\n{1}\n{2}", dealer.Name,
        //        dealer.ToStringOfHand(), dealer.CurrentValue().First());


        //}

        private void Payout(Player[] players, Dealer dealer)
        {

            var tmp = new List<Player>(players);

            var sb = new StringBuilder();
            foreach (Card card in dealer.Hand)
            {
                sb.AppendFormat("{0} - {1}\n", card.CardFace, card.Suit);
            }

            Utility.WriteLine("\n\nDEALER ({1}) \n{0} ", sb, dealer.Hand.Final);

            foreach (var player in players  )
            {

                Utility.WriteLine("Player: {0}({2}) \n{1} ", player.Name,
                    player.ToStringOfHand(), player.Hand.Final);
                
            }


            
            if(dealer.Hand.IsBlackjack)
            {
                Utility.WriteLine("dealer got blackjack");
                var ties = tmp.Where(x => x.Hand.IsBlackjack).ToArray();
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

            var busted = tmp.Where(x => x.Hand.IsBusted).ToArray();
            if(busted.Count() > 0)
            {
                foreach (Player player in busted)
                {
                    player.Payout(-1);
                    tmp.Remove(player);
                }

            }



            if (dealer.Hand.IsBusted)
            {
                Utility.WriteLine("Dealer busted");
                var winners = tmp.Where(x => !x.Hand.IsBusted).ToArray();
                foreach (Player player in winners)
                {
                    player.Payout(1);
                    tmp.Remove(player);
                }
                
                dealer.Payout(-1);
            }
            else
            {


                
                var winners = tmp.Where(x => x.Hand.Final > dealer.Hand.Final && !x.Hand.IsBusted).ToArray();
                
                foreach (Player player in winners)
                {
                    player.Payout(1);
                    tmp.Remove(player);

                }


                var ties = tmp.Where(x => x.Hand.Final == dealer.Hand.Final && !x.Hand.IsBusted).ToArray();
                
                

                foreach (Player player in ties)
                {
                    
                    player.Payout(0);
                    tmp.Remove(player);

                }


                var losers = tmp.Where(x => !x.Hand.IsBusted);
                losers.ForEach(x => x.Payout(-1));

            }

            _dealer.Reset();




        }

        private void PlayHand(Player player)
        {
            //if(player.GetType() == typeof(Dealer))
            //{
            //    var dealer = (Dealer) player;
            //    dealer.SetCard(Suit.Clubs, CardFace.Four, 0);
            //    dealer.SetCard(Suit.Clubs, CardFace.Four, 1);
            //}

            //Card card = new Card(Suit.Clubs, CardFace.Ace);

            PlayAction play = player.Play(this._dealer.TopCard);
            if (play == PlayAction.Stay)
                return;
            if(play == PlayAction.Hit)
            {
                var c = _dealer.Deal();
                var bust = player.TakeCard(c);
                if(!bust)
                    this.PlayHand(player);
            }
            else if(play == PlayAction.Double)
            {
                if (player.Hand.Count() != 2)
                    throw new Exception("only double on first");
                player.TakeCard(_dealer.Deal());
            }

        }

        public void ReportStats()
        {
            _players.Where(x => null != x).ToArray().ForEach(x => Console.Out.WriteLine(x.ToString()));
            Console.Out.WriteLine(_dealer.ToString());
            
        }
    }
}