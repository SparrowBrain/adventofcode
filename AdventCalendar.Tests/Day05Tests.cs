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
        [Theory, AutoData]
        public void PolymerReactor_SameTypeDifferentPolarity_Reacts(Mock<IInputReader> inputReaderMock)
        {
            var polymer = "aA";
            var reactor = new PolymerReactor(polymer);

            var result = reactor.ReactPolymerUnits();

            Assert.Equal("", result);
        }

        [Theory, AutoData]
        public void PolymerReactor_NoAdjecentUnitsOfSameType_NothingHappens(Mock<IInputReader> inputReaderMock)
        {
            var polymer = "abAB";
            var reactor = new PolymerReactor(polymer);

            var result = reactor.ReactPolymerUnits();

            Assert.Equal(polymer, result);
        }

        [Theory, AutoData]
        public void PolymerReactor_AdjecentUnitsOfSameTypeHaveSamePolarity_NothingHappens(Mock<IInputReader> inputReaderMock)
        {
            var polymer = "aabAAB";
            var reactor = new PolymerReactor(polymer);

            var result = reactor.ReactPolymerUnits();

            Assert.Equal(polymer, result);
        }

        [Theory, AutoData]
        public void PolymerReactor_AfterFirstReactionTwoOtherUnitsCanReact_BothReactionsHappen(Mock<IInputReader> inputReaderMock)
        {
            var polymer = "abBA";
            var reactor = new PolymerReactor(polymer);

            var result = reactor.ReactPolymerUnits();

            Assert.Equal("", result);
        }

        [Theory, AutoData]
        public void PolymerReactor_ReactsAfterRemovingReacionStopper(Mock<IInputReader> inputReaderMock)
        {
            var polymer = "dabAcCaCBAcCcaDA";
            var reactor = new PolymerReactor(polymer);

            var result = reactor.ReactAfterRemovingReactionStopper();

            Assert.Equal("dabAaBAaDA", result);
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

        [Theory(Skip ="later"), AutoData]
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