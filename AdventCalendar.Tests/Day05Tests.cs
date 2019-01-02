using AdventCalendar.Day04;
using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using AdventCalendar.Day05;
using Xunit;

namespace AdventCalendar.Tests
{
    public class Day05Tests
    {
        [Fact]
        public void PolymerReactor_SameTypeDifferentPolarity_Reacts()
        {
            var polymer = Unit.ConvertPolymer("aA");
            var reactor = new PolymerReactor();

            var result = reactor.ReactPolymerUnits(polymer);

            Assert.Empty(result);
        }

        [Fact]
        public void PolymerReactor_NoAdjecentUnitsOfSameType_NothingHappens()
        {
            var polymer = Unit.ConvertPolymer("abAB");
            var reactor = new PolymerReactor();

            var result = reactor.ReactPolymerUnits(polymer);

            Assert.Equal(polymer, result);
        }

        [Fact]
        public void PolymerReactor_AdjecentUnitsOfSameTypeHaveSamePolarity_NothingHappens()
        {
            var polymer = Unit.ConvertPolymer("aabAAB");
            var reactor = new PolymerReactor();

            var result = reactor.ReactPolymerUnits(polymer);

            Assert.Equal(polymer, result);
        }

        [Fact]
        public void PolymerReactor_AfterFirstReactionTwoOtherUnitsCanReact_BothReactionsHappen()
        {
            var polymer = Unit.ConvertPolymer("abBA");
            var reactor = new PolymerReactor();

            var result = reactor.ReactPolymerUnits(polymer);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData('a', 'a')]
        [InlineData('B', 'b')]
        public void UnitFactory_ConvertPolymer_ConvertsToCorrectType(char initialUnit, char expectedUnitType)
        {
            var polymer = initialUnit.ToString();

            var units = Unit.ConvertPolymer(polymer);

            Assert.Equal(expectedUnitType, units.First().Type);
        }

        [Theory]
        [InlineData('a', Polarity.Down)]
        [InlineData('B', Polarity.Up)]
        public void UnitFactory_ConvertPolymer_ConvertsToCorrectPolarity(char initialUnit, Polarity unitPolarity)
        {
            var polymer = initialUnit.ToString();

            var units = Unit.ConvertPolymer(polymer);

            Assert.Equal(unitPolarity, units.First().Polarity);
        }

        [Theory, AutoData]
        public void GetsTheCorrectPolymerLengthAfterAllReactions(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "dabAcCaCBAcCcaDA"
            });

            var puzzle = new Day05Puzzle01(inputReaderMock.Object);

            var result = puzzle.Solve();

            Assert.Equal("10", result);
        }

        [Theory, AutoData]
        public void ShortestPolymerLengthAfterRemovingTheProblematicUnit(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "dabAcCaCBAcCcaDA"
            });

            var puzzle = new Day05Puzzle02(inputReaderMock.Object);

            var result = puzzle.Solve();

            Assert.Equal("4", result);
        }


    }
}