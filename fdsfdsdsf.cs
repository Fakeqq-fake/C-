using System;
using System.Collections.Generic;

class Calculator
{
    public event Action<string> OnResult;
    public double Calculate(double a, double b, Func<double, double, double> operation)
    {
        double result = operation(a, b);

        string message = $"{a} ? {b} = {result}";

        OnResult?.Invoke(message);

        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== КАЛЬКУЛЯТОР С ИСТОРИЕЙ ОПЕРАЦИЙ ===");

        Calculator calculator = new Calculator();

        List<string> history = new List<string>();

        calculator.OnResult += (msg) =>
            history.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {msg}");

        Func<double, double, double> add = (x, y) => x + y;
        Func<double, double, double> subtract = (x, y) => x - y;
        Func<double, double, double> power = (x, y) => Math.Pow(x, y);

        Console.WriteLine("\nВыполняем операции:");
        double result1 = calculator.Calculate(10, 5, add);
        Console.WriteLine($"10 + 5 = {result1}");

        double result2 = calculator.Calculate(10, 5, subtract);
        Console.WriteLine($"10 - 5 = {result2}");

        double result3 = calculator.Calculate(2, 3, power);
        Console.WriteLine($"2 ^ 3 = {result3}");

        Console.WriteLine("\n=== ИСТОРИЯ ОПЕРАЦИЙ ===");
        foreach (string logEntry in history)
        {
            Console.WriteLine(logEntry);
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
