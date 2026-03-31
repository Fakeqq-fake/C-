using System;

public delegate string StringFilter(string input);

class Program
{
    static string ToUpperCase(string input)
    {
        return input.ToUpper();
    }

    static string ToStars(string input)
    {
        char[] stars = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            stars[i] = '-';
        }
        return new string(stars);
    }

    static void ProcessString(string input, StringFilter filter)
    {
        string result = filter(input);
        Console.WriteLine(result);
    }

    static void Main(string[] args)
    {
        StringFilter myFilter;

        myFilter = ToUpperCase;
        ProcessString("Hello Wordl", myFilter);

        myFilter = ToStars;
        ProcessString("Hello Wordl", myFilter);
    }
}
