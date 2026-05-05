using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

class Order
{
    public int Id { get; set; }
    public string Product { get; set; }
    public double Price { get; set; }
}

interface IOrderRepository
{
    void Save(Order order);
    List<Order> GetAll();
}

interface ILogger
{
    void Log(string message);
}

interface IOrderService
{
    void CreateOrder(Order order);
    List<Order> GetOrders();
}

class FileOrderRepository : IOrderRepository
{
    private const string FilePath = "orders.json";

    public void Save(Order order)
    {
        var orders = GetAll();
        orders.Add(order);
        var json = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public List<Order> GetAll()
    {
        if (!File.Exists(FilePath))
            return new List<Order>();

        var json = File.ReadAllText(FilePath);
        return string.IsNullOrEmpty(json) ? new List<Order>() : JsonSerializer.Deserialize<List<Order>>(json);
    }
}

class MemoryOrderRepository : IOrderRepository
{
    private List<Order> _orders = new List<Order>();

    public void Save(Order order) => _orders.Add(order);

    public List<Order> GetAll() => _orders;
}

class FileLogger : ILogger
{
    private const string LogPath = "app.log";

    public void Log(string message)
    {
        var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
        File.AppendAllText(LogPath, logEntry);
    }
}

class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
}

class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;

    public OrderService(IOrderRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public void CreateOrder(Order order)
    {
        try
        {
            if (order.Price <= 0)
            {
                _logger.Log("Ошибка: цена заказа должна быть больше 0.");
                throw new ArgumentException("Цена заказа должна быть больше 0.");
            }
            _repository.Save(order);
            _logger.Log($"Заказ на товар '{order.Product}' сохранён.");
        }
        catch (Exception ex)
        {
            _logger.Log($"Ошибка при создании заказа: {ex.Message}");
            throw;
        }
    }

    public List<Order> GetOrders() => _repository.GetAll();
}

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();

        services.AddTransient<IOrderRepository, FileOrderRepository>();
        services.AddSingleton<ILogger, FileLogger>();
        services.AddTransient<IOrderService, OrderService>();

        var provider = services.BuildServiceProvider();
        var service = provider.GetService<IOrderService>();

        while (true)
        {
            Console.WriteLine("\n1. Добавить заказ");
            Console.WriteLine("2. Показать заказы");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите действие: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddOrder(service);
                    break;
                case "2":
                    ShowOrders(service);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void AddOrder(IOrderService service)
    {
        try
        {
            Console.Write("Введите название товара: ");
            var product = Console.ReadLine();
            Console.Write("Введите цену: ");
            if (!double.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Ошибка: некорректный формат цены.");
                return;
            }

            var order = new Order { Id = new Random().Next(1, 1000), Product = product, Price = price };
            service.CreateOrder(order);
            Console.WriteLine("Заказ успешно добавлен.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static void ShowOrders(IOrderService service)
    {
        try
        {
            var orders = service.GetOrders();
            Console.WriteLine("\nСписок заказов:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Товар: {order.Product}, Цена: {order.Price}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении заказов: {ex.Message}");
        }
    }
}
