using System;

public static class MathUtils
{
    public static bool IsEven(int n)
    {
        return n % 2 == 0;
    }

    public static long Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentException("Факториал не определён для отрицательных чисел");

        if (n == 0 || n == 1)
            return 1;

        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    public static double Power(double a, int b)
    {
        if (b == 0)
            return 1;

        double result = 1;
        int absB = Math.Abs(b);

        double baseValue = a;
        while (absB > 0)
        {
            if (absB % 2 == 1)
                result *= baseValue;
            baseValue *= baseValue;
            absB /= 2;
        }

        if (b < 0)
            result = 1 / result;

        return result;
    }

    public static int Clamp(int value, int min, int max)
    {
        if (min > max)
            throw new ArgumentException("Минимальное значение не может быть больше максимального");

        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Тестирование статического класса MathUtils:\n");

        Console.WriteLine($"IsEven(4): {MathUtils.IsEven(4)}");
        Console.WriteLine($"IsEven(7): {MathUtils.IsEven(7)}");
        Console.WriteLine();

        Console.WriteLine($"Factorial(0): {MathUtils.Factorial(0)}");
        Console.WriteLine($"Factorial(5): {MathUtils.Factorial(5)}");
        Console.WriteLine($"Factorial(10): {MathUtils.Factorial(10)}");
        Console.WriteLine();

        Console.WriteLine($"Power(2, 3): {MathUtils.Power(2, 3)}");
        Console.WriteLine($"Power(5, 0): {MathUtils.Power(5, 0)}");
        Console.WriteLine($"Power(2, -3): {MathUtils.Power(2, -3)}");
        Console.WriteLine($"Power(3.5, 2): {MathUtils.Power(3.5, 2)}");
        Console.WriteLine();

        Console.WriteLine($"Clamp(15, 10, 20): {MathUtils.Clamp(15, 10, 20)}");
        Console.WriteLine($"Clamp(5, 10, 20): {MathUtils.Clamp(5, 10, 20)}");
        Console.WriteLine($"Clamp(25, 10, 20): {MathUtils.Clamp(25, 10, 20)}");
        Console.WriteLine($"Clamp(10, 10, 20): {MathUtils.Clamp(10, 10, 20)}");
    }
}
