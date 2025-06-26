namespace Palindrome.CMD;

public class PalindromeService
{
    public static bool IsPalindrome(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        string trimmed = input.Replace(" ", "").ToLower();
        int length = trimmed.Length;

        for (int i = 0; i < length / 2; i++)
        {
            if (trimmed[i] != trimmed[length - i - 1])
                return false;
        }

        return true;
    }
}