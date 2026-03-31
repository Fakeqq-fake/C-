using System;

class Car
{
    public string Brand;
    public string Model;
    public int Year;
    public double EngineVolume;
    public bool IsRunning;
    public int CurrentSpeed;

    public Car(string brand, string model, int year, double engineVolume)
    {
        Brand = brand;
        Model = model;
        Year = year;
        EngineVolume = engineVolume;
        IsRunning = false;
        CurrentSpeed = 0;
    }

    public void StartEngine()
    {
        IsRunning = true;
        Console.WriteLine("Двигатель запущен.");
    }

    public void StopEngine()
    {
        IsRunning = false;
        CurrentSpeed = 0;
        Console.WriteLine("Двигатель остановлен.");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Brand: {Brand}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Year: {Year}");
        Console.WriteLine($"Engine Volume: {EngineVolume} L");
        Console.WriteLine($"Is Running: {IsRunning}");
        Console.WriteLine($"Current Speed: {CurrentSpeed} km/h");
        Console.WriteLine("------------------------");
    }

    public void Accelerate(int speed)
    {
        if (!IsRunning)
        {
            Console.WriteLine("Сначала запустите двигатель!");
            return;
        }

        CurrentSpeed += speed;
        if (CurrentSpeed < 0)
            CurrentSpeed = 0;

        Console.WriteLine($"Текущая скорость: {CurrentSpeed} км/ч");
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car("Toyota", "Corolla", 2020, 1.8);
        Car car2 = new Car("Honda", "Civic", 2019, 2.0);
        Car car3 = new Car("BMW", "X5", 2021, 3.0);

        car1.DisplayInfo();

        car1.StartEngine();
