using System;
using System.Diagnostics;
using System.Linq;

namespace Blackjack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var table = new Table();
            Player[] players = {
                new Player("Book Guy 1", new ByTheBookStrategy()),
                new Player("Book Guy 2", new ByTheBookStrategy()),
                //new Player("Bad guy 1", new ImbicileStrategy()),
                //new Player("Mad man", new RandomStrategy()),
            };
            var dealer = new Player("Dealer", new DealerStrategy());
            var shoe = new Shoe(7);
            for (var i = 0; i < 10000; i++)
            {
                if(shoe.NeedsNewShoe())
                    shoe = new Shoe(7);
                table.PlayHand(shoe, players, dealer);
            }
            ReportStats(players, dealer);
            Console.Read();
        }

        public static void ReportStats(Player[] players, Player dealer)
        {
            players.Where(x => null != x).ToArray().ForEach(x => Console.Out.WriteLine(x.ToString()));
            Console.Out.WriteLine(dealer.ToString());
        }

    }
}