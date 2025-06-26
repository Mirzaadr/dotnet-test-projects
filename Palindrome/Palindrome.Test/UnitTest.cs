using Palindrome.CMD;

namespace Palindrome.Test;


public class PalindromeCheckerTests
{
    [Theory]
    [InlineData("racecar", true)]
    [InlineData("RaceCar", true)]
    [InlineData("hello", false)]
    [InlineData("A man a plan a canal Panama", true)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData(null, false)]
    public void IsPalindrome_ReturnsExpectedResult(string? input, bool expected)
    {
        bool result = PalindromeService.IsPalindrome(input);
        Assert.Equal(expected, result);
    }
}
