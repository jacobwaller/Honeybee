using System;
using FluentAssertions;
using Xunit;

namespace Honeybee.Test.Int
{
    public class Calculator_Tests
    {
        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(3, 3, 6)]
        [InlineData(3, 2, 5)]
        public void AddsNumbers(int a, int b, int result)
        {
            Calculator.Add(a, b).Should().Be(result);
        }

        //TODO: Add other integration tests
    }
}
