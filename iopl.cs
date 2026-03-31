using System;

class Employee
{
    public string Name { get; set; }
    public virtual decimal CalculateSalary()
    {
        return 0m;
    }

    public Employee(string name)
    {
        Name = name;
    }
}

class Teacher : Employee
{
    public decimal BaseSalary { get; set; } 
    public decimal Bonus { get; set; } 
    public Teacher(string name, decimal baseSalary, decimal bonus)
        : base(name)
    {
        BaseSalary = baseSalary;
        Bonus = bonus;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary + Bonus;
    }
}

class Administrator : Employee
{
    public decimal FixedSalary { get; set; }

    public Administrator(string name, decimal fixedSalary)
        : base(name) 
    {
        FixedSalary = fixedSalary;
    }
    public override decimal CalculateSalary()
    {
        return FixedSalary;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== РАСЧЁТ ЗАРПЛАТ СОТРУДНИКОВ ===");

        Employee[] employees = new Employee[]
        {
            new Teacher("Анна Иванова", 30000m, 5000m), 
            new Administrator("Пётр Сидоров", 45000m), 
            new Teacher("Мария Петрова", 32000m, 6000m),
            new Administrator("Иван Козлов", 48000m) 
        };

        foreach (Employee employee in employees)
        {
            decimal salary = employee.CalculateSalary();
            Console.WriteLine($"{employee.Name}: {salary:C}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
