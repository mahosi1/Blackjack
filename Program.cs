using System;
using System.Diagnostics;

namespace Blackjack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //File.Delete(@"C:\tmp\test.txt");
            //var k = new TextWriterTraceListener(@"C:\tmp\test.txt");
            //Trace.Listeners.Add(k);
            Trace.Listeners.Add(new ConsoleTraceListener());

            var table = new Table(5, 10);
            table.AddPlayer(new ByTheBookPlayer("Book Guy 1"));
            //table.AddPlayer(new ImbicilePlayer("Bad guy 1"));
            table.AddPlayer(new ByTheBookPlayer("Book Guy 2"));
            table.AddPlayer(new ImbicilePlayer("Bad guy 1"));
            table.AddPlayer(new RandomPlayer("Mad man"));
            for (var i = 0; i < 10000; i++)
            {
                table.PlayHand();
            }
            table.ReportStats();
            Console.Read();
        }
    }
}