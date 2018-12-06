using System;
using System.Runtime.CompilerServices;
using AdventCalendar.Day01;

[assembly: InternalsVisibleTo("AdventCalendar.Tests")]

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Task01().Solve("Day01\\input1-1.txt");
            //Console.WriteLine(new Task02().Solve("Day01\\input1-1.txt"));

            var task = new Day02.Day02Task01(new Day02.SymbolAnalyzer());

            var result = task.Solve();

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
