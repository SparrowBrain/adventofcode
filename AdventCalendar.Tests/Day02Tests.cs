using System;
using System.Collections.Generic;
using System.Text;
using AdventCalendar.Day02;
using Xunit;

namespace AdventCalendar.Tests
{
    public class Day02Tests
    {
        [Theory]
        [InlineData("abcdef", false, false)]
        [InlineData("bababc", true, true)]
        [InlineData("abbcde", true, false)]
        [InlineData("abcccd", false, true)]
        [InlineData("aabcdd", true, false)]
        [InlineData("abcdee", true, false)]
        [InlineData("ababab", false, true)]
        public void SymbolAnalyzer__ReturnsCorrectCount(string line, bool twoCount, bool threeCount)
        {
            var analyzer = new SymbolAnalyzer();

            var counts = analyzer.Analyze(line);

            Assert.Equal((twoCount, threeCount), counts);
        }
    }
}
