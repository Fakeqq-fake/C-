using System;

class BankAccount
{
    private decimal balance;

    public decimal Balance
    {
        get { return balance; }
    }

    public BankAccount()
    {
        balance = 0m;
    }

    public BankAccount(decimal initialBalance)
    {
        if (initialBalance < 0)
            throw new ArgumentException("Начальный баланс не может быть отрицательным");
        balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сумма пополнения должна быть больше нуля");

        balance += amount;
        Console.WriteLine($"Пополнение: +{amount}. Новый баланс: {balance}");
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сумма снятия должна быть больше нуля");

        if (amount > balance)
        {
            Console.WriteLine($"Ошибка: недостаточно средств для снятия {amount}. Текущий баланс: {balance}");
            return false;
        }

        balance -= amount;
        Console.WriteLine($"Снятие: -{amount}. Новый баланс: {balance}");
        return true;
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount(1000m);

        Console.WriteLine($"Начальный баланс: {account.Balance}");

        account.Deposit(500m);

        account.Withdraw(200m);

        account.Withdraw(2000m);

        account.Deposit(100m);

        Console.WriteLine($"Финальный баланс: {account.Balance}");

    }
}
