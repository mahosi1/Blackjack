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
            table.AddPlayer("Book Guy 1", new ByTheBookStrategy());
            //table.AddPlayer("Bad guy 1", new ImbicilePlayer());
            table.AddPlayer("Book Guy 2", new ByTheBookStrategy());
            //table.AddPlayer("Bad guy 1", new ImbicileStrategy());
            //table.AddPlayer("Mad man", new RandomStrategy());
            for (var i = 0; i < 10000; i++)
            {
                table.PlayHand();
            }
            table.ReportStats();
            Console.Read();
        }
    }
}