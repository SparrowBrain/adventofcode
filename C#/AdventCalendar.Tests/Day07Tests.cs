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
        public void Day07Puzzle01_SolvePuzzliInCorrectOrder(Mock<IInputReader> inputReaderMock, Settings settings)
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

            var puzzle = new Day07Puzzle01(inputReaderMock.Object, settings);

            var result = puzzle.Solve();

            Assert.Equal("CABDFE", result);
        }

        [Theory]
        [AutoData]
        public void Day07Puzzle02_CalculateTheSecondsItTakesToSolvePuzzle(Mock<IInputReader> inputReaderMock, Settings settings)
        {
            settings.StepSettings.DurationOffset = 0;
            settings.WorkerSettings.WorkerCount = 2;
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

            var puzzle = new Day07Puzzle02(inputReaderMock.Object, settings);

            var result = puzzle.Solve();

            Assert.Equal("15", result);
        }

        [Theory]
        [InlineAutoData("Step C must be finished before step A can begin.", 'C', 'A')]
        [InlineAutoData("Step C must be finished before step F can begin.", 'C', 'F')]
        [InlineAutoData("Step F must be finished before step E can begin.", 'F', 'E')]
        public void Step_ParsesLineToCorrectStepsWithPrerequisiteAndNext(string line, char step1, char step2, StepSettings stepSettings)
        {
            var lines = new List<string> {
                line
            };

            var steps = new StepFactory(stepSettings).Create(lines);

            Assert.Equal(2, steps.Count);
            Assert.Equal(step2, steps.First(x => x.Name == step1).NextSteps.First().Name);
            Assert.Empty(steps.First(x => x.Name == step2).NextSteps);
            Assert.Equal(step1, steps.First(x => x.Name == step2).PrerequisiteSteps.First().Name);
            Assert.Empty(steps.First(x => x.Name == step1).PrerequisiteSteps);
        }

        [Theory]
        [AutoData]
        public void Step_ParsesTwoLinesWithThreeSteps(StepSettings stepSettings)
        {
            var lines = new List<string> {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
            };

            var steps = new StepFactory(stepSettings).Create(lines);

            Assert.Equal(3, steps.Count);

            Assert.Contains(steps.First(x => x.Name == 'A').PrerequisiteSteps, x => x.Name == 'C');
            Assert.Contains(steps.First(x => x.Name == 'F').PrerequisiteSteps, x => x.Name == 'C');

            Assert.Contains(steps.First(x => x.Name == 'C').NextSteps, x => x.Name == 'A');
            Assert.Contains(steps.First(x => x.Name == 'C').NextSteps, x => x.Name == 'F');
        }

        [Theory]
        [AutoData]
        public void InstructionHelper_IdentifiesTheFirstStep(StepSettings stepSettings)
        {
            var lines = new List<string> {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
            };

            var steps = new StepFactory(stepSettings).Create(lines);
            var instructionHelper = new InstructionHelper();

            var result = instructionHelper.Order(steps);

            Assert.Equal('C', result.First());
        }

        [Theory]
        [AutoData]
        public void InstructionHelper_PicksNextStepAlpabeticallyFromAllAvailable(StepSettings stepSettings)
        {
            var lines = new List<string> {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
            };

            var steps = new StepFactory(stepSettings).Create(lines);
            var instructionHelper = new InstructionHelper();

            var result = instructionHelper.Order(steps);

            Assert.Equal("CABF", result);
        }

        [Theory]
        [InlineAutoData("Step A must be finished before step H can begin.", 'A', 1)]
        [InlineAutoData("Step B must be finished before step H can begin.", 'B', 2)]
        [InlineAutoData("Step C must be finished before step H can begin.", 'C', 3)]
        [InlineAutoData("Step Z must be finished before step H can begin.", 'Z', 26)]
        [InlineAutoData("Step A must be finished before step Z can begin.", 'Z', 26)]
        public void StepFactory_AssignsStepDuration(string line, char testedStep, int duration, StepSettings stepSettings)
        {
            stepSettings.DurationOffset = 0;
            var lines = new List<string> {
                line
            };

            var steps = new StepFactory(stepSettings).Create(lines);

            Assert.Equal(duration, steps.First(x => x.Name == testedStep).Duration);
        }

        [Theory]
        [InlineAutoData("Step A must be finished before step H can begin.", 'A', 61, 60)]
        [InlineAutoData("Step B must be finished before step H can begin.", 'B', 62, 60)]
        [InlineAutoData("Step C must be finished before step H can begin.", 'C', 63, 60)]
        [InlineAutoData("Step Z must be finished before step H can begin.", 'Z', 86, 60)]
        [InlineAutoData("Step A must be finished before step Z can begin.", 'Z', 86, 60)]
        public void StepFactory_AllowsSettingDurationOffset(string line, char testedStep, int duration, int durationOffset, StepSettings stepSettings)
        {
            stepSettings.DurationOffset = durationOffset;
            var lines = new List<string> {
                line
            };

            var steps = new StepFactory(stepSettings).Create(lines);

            Assert.Equal(duration, steps.First(x => x.Name == testedStep).Duration);
        }

        [Theory]
        [InlineAutoData("Step A must be finished before step Z can begin.", 1 + 26)]
        [InlineAutoData("Step B must be finished before step A can begin.", 2 + 1)]
        [InlineAutoData("Step C must be finished before step A can begin.", 3 + 1)]
        public void InstructionHelper_TimeToAssemble_OneWorkerSolvesTwoSteps(string line, int duration, StepSettings stepSettings, WorkerSettings workerSettings)
        {
            stepSettings.DurationOffset = 0;
            workerSettings.WorkerCount = 1;
            var lines = new List<string> {
                line
            };

            var steps = new StepFactory(stepSettings).Create(lines);
            var instructionHelper = new InstructionHelper();

            var result = instructionHelper.TimeToAssemble(steps, workerSettings.WorkerCount);

            Assert.Equal(duration, result);
        }

        [Theory]
        [InlineAutoData("Step A must be finished before step Z can begin.", 1 + 26)]
        [InlineAutoData("Step B must be finished before step A can begin.", 2 + 1)]
        [InlineAutoData("Step C must be finished before step A can begin.", 3 + 1)]
        public void InstructionHelper_TimeToAssemble_TwoWorkersSolvesTwoBlockedSteps(string line, int duration, StepSettings stepSettings, WorkerSettings workerSettings)
        {
            stepSettings.DurationOffset = 0;
            workerSettings.WorkerCount = 2;
            var lines = new List<string> {
                line
            };

            var steps = new StepFactory(stepSettings).Create(lines);
            var instructionHelper = new InstructionHelper();

            var result = instructionHelper.TimeToAssemble(steps, workerSettings.WorkerCount);

            Assert.Equal(duration, result);
        }

        [Theory]
        [AutoData]
        public void InstructionHelper_TimeToAssemble_TwoWorkersSolvesThreeSteps(StepSettings stepSettings, WorkerSettings workerSettings)
        {
            stepSettings.DurationOffset = 0;
            workerSettings.WorkerCount = 2;
            var lines = new List<string> {
                "Step C must be finished before step B can begin.",
                "Step C must be finished before step F can begin.",
            };

            var steps = new StepFactory(stepSettings).Create(lines);
            var instructionHelper = new InstructionHelper();

            var result = instructionHelper.TimeToAssemble(steps, workerSettings.WorkerCount);

            Assert.Equal(9, result);
        }
    }
}