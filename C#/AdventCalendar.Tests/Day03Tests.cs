using System.Collections.Generic;
using System.Linq;
using AdventCalendar.Day03;
using Xunit;

namespace AdventCalendar.Tests
{
    public class Day03Tests
    {
        [Fact]
        public void FabricChecker_CountsConflictingClaims()
        {
            var fabricChecker = new FabricChecker();
            var claims = new List<Claim>
            {
                new Claim("1", new Coordinate(1, 3), new Coordinate(4, 4)),
                new Claim("2", new Coordinate(3, 1), new Coordinate(4, 4)),
                new Claim("3", new Coordinate(5, 5), new Coordinate(2, 2)),
            };

            var count = fabricChecker.GetOverlapNumber(claims);

            Assert.Equal(4, count);
        }

        [Theory]
        [InlineData("#1 @ 1,3: 4x4", "1", 1, 3, 4, 4)]
        [InlineData("#2 @ 3,1: 4x4", "2", 3, 1, 4, 4)]
        [InlineData("#3 @ 5,5: 2x2", "3", 5, 5, 2, 2)]
        public void Parser_Works(string line, string id, int xPos, int yPos, int xSize, int ySize)
        {
            var parser = new Parser();

            var claim = parser.Parse(new List<string> { line }).First();

            Assert.Equal(id, claim.Id);
            Assert.Equal(xPos, claim.Pos.X);
            Assert.Equal(yPos, claim.Pos.Y);
            Assert.Equal(xSize, claim.Size.X);
            Assert.Equal(ySize, claim.Size.Y);
        }

        [Fact]
        public void FabricChecker_FindsNonOverlappingClaim()
        {
            var fabricChecker = new FabricChecker();
            var claims = new List<Claim>
            {
                new Claim("1", new Coordinate(1, 3), new Coordinate(4, 4)),
                new Claim("2", new Coordinate(3, 1), new Coordinate(4, 4)),
                new Claim("3", new Coordinate(5, 5), new Coordinate(2, 2)),
            };

            var id = fabricChecker.GetNonOverlappingId(claims);

            Assert.Equal("3", id);
        }
    }
}