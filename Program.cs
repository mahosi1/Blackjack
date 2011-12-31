using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {

            //Trace.Listeners.Add(new ConsoleTraceListener());

            var table = new Table(5, 10);
            table.AddPlayer(new ByTheBook("Joe"));
            table.AddPlayer(new ByTheBook("Jill"), 4);
            for (int i = 0; i < 1000; i++)
                table.PlayHand(); 
   
            table.ReportStats();
            Console.Read();

        }
    }
}
