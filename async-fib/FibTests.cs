using FluentAssertions;
using Xunit;

namespace async_fib;

public class FibTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(10, 55)]
    public void FibSync_Should_CorrectlyCalculateTheNthFibNumber(int n, int expectedResult)
    {
        var result = FibSolutions.FibSync(n);
        
        result.Should().Be(expectedResult);
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(10, 55)]
    public async Task FibAsync_Should_CorrectlyCalculateTheNthFibNumber(int n, int expectedResult)
    {
        var result = await FibSolutions.FibAsync(n);
        
        result.Should().Be(expectedResult);
    }
}