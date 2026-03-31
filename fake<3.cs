using System;

class Student
{
    public string Name { get; }

    public int Age { get; set; }

    private double _gpa;
    public double GPA
    {
        get { return _gpa; }
        set
        {
            if (value < 0.0 || value > 4.0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Значения от 0.0 до 4.0!");
            }
            _gpa = value;
        }
    }

    public Student(string name, int age, double gpa)
    {
        Name = name;
        Age = age;
        GPA = gpa;
    }
}

class BankAccount
{
    public string AccountNumber { get; }

    private decimal _balance;
    public decimal Balance
    {
        get { return _balance; }
    }

    public string OwnerName { get; set; }

    public BankAccount(string accountNumber, string ownerName, decimal initialBalance = 0m)
    {
        AccountNumber = accountNumber;
        OwnerName = ownerName;
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сумма пополнения должна быть положительной.");
        }
        _balance += amount;
        Console.WriteLine($"Счёт пополнен на {amount:C}. Новый баланс: {_balance:C}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сумма снятия должна быть положительной.");
        }

        if (amount > _balance)
        {
            throw new InvalidOperationException("Недостаточно средств на счёте!");
        }

        _balance -= amount;
        Console.WriteLine($"Снято {amount:C}. Остаток на счёте: {_balance:C}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ КЛАССА STUDENT ===");

        try
        {
            Student student = new Student("Анна Иванова", 20, 3.8);
            Console.WriteLine($"Студент: {student.Name}, Возраст: {student.Age}, GPA: {student.GPA}");

            student.Age = 21;
            Console.WriteLine($"Новый возраст: {student.Age}");

            Console.WriteLine("\nПопытка установить GPA = 5.0:");
            student.GPA = 5.0;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ КЛАССА BANKACCOUNT ===");

        try
        {
            BankAccount account = new BankAccount("ACC123456", "Анна Иванова", 1000m);
            Console.WriteLine($"Владелец: {account.OwnerName}");
            Console.WriteLine($"Номер счёта: {account.AccountNumber}");
            Console.WriteLine($"Баланс: {account.Balance:C}");

            account.Deposit(500m);

            account.Withdraw(200m);

            Console.WriteLine("\nПопытка снять 2000:");
            account.Withdraw(2000m);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
