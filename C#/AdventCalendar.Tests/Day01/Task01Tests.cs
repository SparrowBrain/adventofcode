using System;
using System.Collections.Generic;
using System.Text;
using AdventCalendar.Day01;
using Xunit;

namespace AdventCalendar.Tests.Day01
{
    public class Task01Tests
    {
        [Fact]
        public void FrequencyParser_ReturnsPositive()
        {
            var result = FrequencyParser.ParseChange("+12");

            Assert.Equal(12, result);
        }

        [Fact]
        public void FrequencyParser_ReturnsNegative()
        {
            var result = FrequencyParser.ParseChange("-3");

            Assert.Equal(-3, result);
        }
    }
}
