using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleApplication7;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {

            File.Delete(@"C:\tmp\test.txt");
            var k = new TextWriterTraceListener(@"C:\tmp\test.txt");
            Trace.Listeners.Add(k);
            Trace.Listeners.Add(new ConsoleTraceListener());

            var table = new Table(5, 10);
            table.AddPlayer(new ByTheBook("Joe"));
            //table.AddPlayer(new ByTheBook("Jill"), 4);
            //table.AddPlayer(new ByTheBook("Josh"), 2);
            for (int i = 0; i < 1000; i++)
                table.PlayHand(); 
   
            table.ReportStats();
            Console.Read();

        }
    }
}
