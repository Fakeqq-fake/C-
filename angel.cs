using System;
using System.Collections.Generic;

class Seat
{
    public decimal Price { get; set; }
    public bool IsBooked { get; set; }

    public Seat(decimal price)
    {
        Price = price;
        IsBooked = false;
    }
}

class CinemaHall
{
    private Seat[,] seats;
    public List<string> log;

    public event Action<string> OnHallFull;

    public CinemaHall()
    {
        seats = new Seat[5, 10];
        log = new List<string>();

        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                decimal basePrice = row < 2 ? 500m : row < 4 ? 400m : 300m;
                seats[row, col] = new Seat(basePrice);
            }
        }
    }

    public void SubscribeToHallFull(Action<string> handler)
    {
        OnHallFull += handler;
    }

    public void ShowAvailable(Predicate<Seat> filter)
    {
        Console.WriteLine("Доступные места:");
        bool found = false;

        for (int row = 0; row < seats.GetLength(0); row++)
        {
            for (int col = 0; col < seats.GetLength(1); col++)
            {
                if (filter(seats[row, col]))
                {
                    Console.WriteLine($"[Ряд {row + 1}, Место {col + 1}] - {seats[row, col].Price:C}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("Подходящих мест не найдено.");
        }
        Console.WriteLine();
    }

    public bool TryBook(int row, int col, Func<int, int, decimal, decimal> priceModifier)
    {

        if (row < 0 || row >= seats.GetLength(0) || col < 0 || col >= seats.GetLength(1))
        {
            string errorMsg = $"Ошибка: Ряд {row + 1} или Место {col + 1} не существует";
            log.Add(errorMsg);
            Console.WriteLine(errorMsg);
            return false;
        }

        Seat seat = seats[row, col];

        if (seat.IsBooked)
        {
            string errorMsg = $"Ошибка: Место [Ряд {row + 1}, Место {col + 1}] уже забронировано";
            log.Add(errorMsg);
            Console.WriteLine(errorMsg);
            return false;
        }

        decimal finalPrice = priceModifier(row, col, seat.Price);
        seat.IsBooked = true;
        seat.Price = finalPrice;

        string successMsg = $"Ряд {row + 1}, Место {col + 1} забронировано за {finalPrice:C}";
        log.Add(successMsg);
        Console.WriteLine(successMsg);

        if (IsHallFull())
        {
            OnHallFull?.Invoke("Зал полностью забронирован!");
        }

        return true;
    }
    private bool IsHallFull()
    {
        for (int row = 0; row < seats.GetLength(0); row++)
        {
            for (int col = 0; col < seats.GetLength(1); col++)
            {
                if (!seats[row, col].IsBooked)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void ShowLog()
    {
        Console.WriteLine("\n=== ЛОГ ДЕЙСТВИЙ ===");
        foreach (string entry in log)
        {
            Console.WriteLine(entry);
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== СИСТЕМА CINEMA MATRIX ===");

        CinemaHall hall = new CinemaHall();

        hall.SubscribeToHallFull((msg) =>
        {
            Console.WriteLine($"ВНИМАНИЕ: {msg}");
            hall.ShowLog();
        });

        Console.WriteLine("Задание A: Поиск и фильтрация");

        hall.ShowAvailable(s => !s.IsBooked);

        hall.ShowAvailable(s => !s.IsBooked && s.Price < 400m);

        Console.WriteLine("Задание B: Бронирование с динамической ценой");

        Func<int, int, decimal, decimal> priceModifier = (row, col, basePrice) =>
        {
            decimal price = basePrice;
            if (row == 4)
            {
                price *= 1.2m; 
            }
            if (col >= 0 && col <= 2)
            {
                price *= 0.9m; 
            }
            return price;
        };

        hall.TryBook(0, 0, priceModifier);
        hall.TryBook(4, 5, priceModifier);
        hall.TryBook(2, 1, priceModifier); 

        hall.ShowAvailable(s => !s.IsBooked);

        hall.ShowLog();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
