using AdventCalendar.Day07;
using AutoFixture.Xunit2;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventCalendar.Tests
{
    public class Day07Tests
    {
        [Theory]
        [AutoData]
        public void Day07Puzzle01_SolvePuzzliInCorrectOrder(Mock<IInputReader> inputReaderMock)
        {
            inputReaderMock.Setup(x => x.ReadLines()).Returns(new List<string>
            {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin.",
            });

            var puzzle = new Day07Puzzle01(inputReaderMock.Object);

            var result = puzzle.Solve();

            Assert.Equal("CABDFE", result);
        }

        [Theory]
        [InlineData("Step C must be finished before step A can begin.", 'C', 'A')]
        [InlineData("Step C must be finished before step F can begin.", 'C', 'F')]
        [InlineData("Step F must be finished before step E can begin.", 'F', 'E')]
        public void Step_ParsesLineToCorrectStepsWithPrerequisiteAndNext(string line, char step1, char step2)
        {
            var lines = new List<string> {
                line
            };

            var steps = Step.Create(lines);

            Assert.Equal(2, steps.Count);
            Assert.Equal(step2, steps.First(x => x.Name == step1).NextSteps.First().Name);
            Assert.Empty(steps.First(x => x.Name == step2).NextSteps);
            Assert.Equal(step1, steps.First(x => x.Name == step2).PrerequisiteSteps.First().Name);
            Assert.Empty(steps.First(x => x.Name == step1).PrerequisiteSteps);
        }

        [Fact]
        public void Step_ParsesTwoLinesWithThreeSteps()
        {
            var lines = new List<string> {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
            };

            var steps = Step.Create(lines);

            Assert.Equal(3, steps.Count);

            Assert.Contains(steps.First(x => x.Name == 'A').PrerequisiteSteps, x => x.Name == 'C');
            Assert.Contains(steps.First(x => x.Name == 'F').PrerequisiteSteps, x => x.Name == 'C');

            Assert.Contains(steps.First(x => x.Name == 'C').NextSteps, x => x.Name == 'A');
            Assert.Contains(steps.First(x => x.Name == 'C').NextSteps, x => x.Name == 'F');
        }

        [Fact]
        public void StepOrderer_IdentifiesTheFirstStep()
        {
            var lines = new List<string> {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
            };

            var steps = Step.Create(lines);
            var stepOrderer = new StepOrderer();

            var result = stepOrderer.Order(steps);

            Assert.Equal('C', result.First());
        }

        [Fact]
        public void StepOrderer_PicksNextStepAlpabeticallyFromAllAvailable()
        {
            var lines = new List<string> {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
            };

            var steps = Step.Create(lines);
            var stepOrderer = new StepOrderer();

            var result = stepOrderer.Order(steps);

            Assert.Equal("CABF", result);
        }
    }
}