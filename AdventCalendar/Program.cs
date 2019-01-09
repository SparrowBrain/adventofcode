using AdventCalendar.Day01;
using AdventCalendar.Day07;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdventCalendar.Tests")]

namespace AdventCalendar
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //new Task01().Solve("Day01\\input1-1.txt");
            //Console.WriteLine(new Task02().Solve("Day01\\input1-1.txt"));

            IPuzzle puzzle;
            //puzzle = new Day02.Day02Task01(new FileReader("Day02\\input.txt"), new Day02.SymbolAnalyzer());
            //puzzle = new Day02.Day02Task02(new FileReader("Day02\\input.txt"));
            //puzzle = new Day03Task01(new FileReader("Day03\\input.txt"));
            //puzzle = new Day03Task02(new FileReader("Day03\\input.txt"));
            //puzzle = new Day04Puzzle02(new FileReader("Day04\\input.txt"));
            //puzzle = new Day05Puzzle01(new FileReader("Day05\\input.txt"));
            //puzzle = new Day05Puzzle02(new FileReader("Day05\\input.txt"));
            //puzzle = new Day06Puzzle01(new FileReader("Day06\\input.txt"));
            //puzzle = new Day06Puzzle02(new FileReader("Day06\\input.txt"), 10000);
            //puzzle = new Day07Puzzle01(new FileReader("Day07\\input.txt"), new Settings{StepSettings=new StepSettings{DurationOffset=60}});
            puzzle = new Day07Puzzle02(new FileReader("Day07\\input.txt"), new Settings { StepSettings = new StepSettings { DurationOffset = 60 }, WorkerSettings = new WorkerSettings { WorkerCount = 5 } });

            var result = puzzle.Solve();

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}