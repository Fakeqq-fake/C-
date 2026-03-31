using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsBanned { get; set; }
    public User(string name, int age, bool isBanned)
    {
        Name = name;
        Age = age;
        IsBanned = isBanned;
    }
    public override string ToString()
    {
        string status = IsBanned ? "Забанен" : "Активен";
        return $"{Name}, {Age} лет, статус: {status}";
    }
}

class Program
{
    static List<User> FilterUsers(List<User> users, Predicate<User> condition)
    {
        List<User> result = new List<User>();

        foreach (User user in users)
        {
            if (condition(user))
            {
                result.Add(user);
            }
        }

        return result;
    }

    static void Main()
    {
        Console.WriteLine("=== ФИЛЬТР СПИСКА ПОЛЬЗОВАТЕЛЕЙ ===");
        List<User> users = new List<User>
        {
            new User("Анна Иванова", 25, false),
            new User("Пётр Сидоров", 17, false),
            new User("Мария Петрова", 30, true),
            new User("Иван Козлов", 19, false),
            new User("Елена Волкова", 16, true)
        };

        Console.WriteLine("\nИсходный список пользователей:");
        foreach (User user in users)
        {
            Console.WriteLine(user);
        }

        Console.WriteLine("\n1. Совершеннолетние пользователи:");
        List<User> adults = FilterUsers(users, user => user.Age >= 18);
        foreach (User user in adults)
        {
            Console.WriteLine(user);
        }

        Console.WriteLine("\n2. Забаненные пользователи:");
        List<User> banned = FilterUsers(users, user => user.IsBanned);
        foreach (User user in banned)
        {
            Console.WriteLine(user);
        }

        Console.WriteLine("\n3. Совершеннолетние забаненные пользователи:");
        List<User> adultBanned = FilterUsers(users,
            user => user.Age >= 18 && user.IsBanned);
        foreach (User user in adultBanned)
        {
            Console.WriteLine(user);
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
