using System;
using System.Runtime.CompilerServices;
using AdventCalendar.Day01;
using AdventCalendar.Day02;
using AdventCalendar.Day03;
using AdventCalendar.Day04;

[assembly: InternalsVisibleTo("AdventCalendar.Tests")]

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Task01().Solve("Day01\\input1-1.txt");
            //Console.WriteLine(new Task02().Solve("Day01\\input1-1.txt"));

            IPuzzle puzzle;
            //puzzle = new Day02.Day02Task01(new Day02.SymbolAnalyzer());
            //puzzle = new Day02.Day02Task02();
            //puzzle = new Day03Task01();
            //puzzle = new Day03Task02();
            puzzle = new Day04Task01(new FileReader());

            var result = puzzle.Solve();

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
