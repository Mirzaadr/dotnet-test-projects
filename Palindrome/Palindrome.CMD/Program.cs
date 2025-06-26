using System;
using Palindrome.CMD;

class Program
{
    static void Main(string[] args)
    {
        string? input;

        // Keep prompting until user enters valid input
        while (true)
        {
            Console.WriteLine("Enter a string to check if it's a palindrome:");

            input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                break;

            Console.WriteLine("Input cannot be empty or whitespace. Please try again.\n");
        }

        bool isPalindrome = PalindromeService.IsPalindrome(input);
        Console.WriteLine(isPalindrome);
    }
}
