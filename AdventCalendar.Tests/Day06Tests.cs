using AdventCalendar.Day06;
using AutoFixture.Xunit2;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventCalendar.Tests
{
    public class Day06Tests
    {
        [Theory, AutoData]
        public void Day06Puzzle01_LargestNonInfiniteArea(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9",
            });

            var puzzle = new Day06Puzzle01(inputReaderMock.Object);

            var result = puzzle.Solve();

            Assert.Equal("17", result);
        }
        
        [Theory]
        [InlineData("1, 1")]
        [InlineData("1, 6")]
        [InlineData("8, 3")]
        [InlineData("3, 4")]
        [InlineData("5, 5")]
        [InlineData("8, 9")]
        public void FieldGenerator_SetsCoordinateUnderTheSource(string source)
        {
            var sources = new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9",
            }.Select(Source.Parse);
            var generator = new FieldGenerator(sources);

            var fields = generator.CreateFields();

            var expectedSource = Source.Parse(source);
            Assert.Equal(expectedSource, fields[expectedSource.X][expectedSource.Y].Source);
            Assert.Equal(0, fields[expectedSource.X][expectedSource.Y].Distance);
        }

        [Theory]
        [InlineData("1, 1", 1, 0)]
        [InlineData("1, 1", 0, 0)]
        [InlineData("8, 3", 6, 1)]
        public void FieldGenerator_SetsCoordinateClosestToOneSource(string closestSource, int x, int y)
        {
            var sources = new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9",
            }.Select(Source.Parse);
            var generator = new FieldGenerator(sources);

            var fields = generator.CreateFields();

            var expectedSource = Source.Parse(closestSource);
            Assert.Equal(expectedSource, fields[x][y].Source);
        }

        [Theory]
        [InlineData(5, 0)]
        [InlineData(5, 1)]
        [InlineData(0, 4)]
        [InlineData(1, 4)]
        [InlineData(2, 5)]
        public void FieldGenerator_SetsConflictForCoordinatesClosestToTwoOrMoreSources(int x, int y)
        {
            var sources = new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9",
            }.Select(Source.Parse);
            var generator = new FieldGenerator(sources);

            var fields = generator.CreateFields();

            Assert.Equal(Vector.Conflict, fields[x][y]);
        }

        [Theory]
        [InlineData("1, 1", true)]
        [InlineData("1, 6", true)]
        [InlineData("8, 3", true)]
        [InlineData("3, 4", false)]
        [InlineData("5, 5", false)]
        [InlineData("8, 9", true)]
        public void FieldGenerator_MarksSourcesWithFieldInfinity(string source, bool expectedInfinity)
        {
            var sources = new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9",
            }.Select(Source.Parse);
            var generator = new FieldGenerator(sources);

            var fields = generator.CreateFields();

            var actualInfinity = fields.SelectMany(x => x.ToList()).First(x => Equals(x.Source, Source.Parse(source))).Source.Infinity;
            Assert.Equal(expectedInfinity, actualInfinity);
        }
    }
}