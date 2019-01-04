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
        [Theory(Skip = "Later"), AutoData]
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

        [Fact]
        public void BoardGetsMaxValues()
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
            var boardFactory = new BoardFactory(sources);

            (var maxX, var maxY) = boardFactory.GetMaxXY();

            Assert.Equal(8, maxX);
            Assert.Equal(9, maxY);
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
        [InlineData("1, 1", 1, 0, 1)]
        [InlineData("1, 1", 0, 0, 2)]
        [InlineData("8, 3", 6, 1, 4)]
        public void FieldGenerator_SetsCoordinateClosestToOneSource(string closestSource, int x, int y, int distance)
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
            Assert.Equal(distance, fields[x][y].Distance);
        }
    }
}