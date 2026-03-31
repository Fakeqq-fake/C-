using System;

class Box<T>
{
    private T _value;


    public void SetValue(T value)
    {
        _value = value;
    }

    public T GetValue()
    {
        return _value;
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"Person: {Name}, {Age} лет";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ КЛАССА Box<T> ===");

        Console.WriteLine("\n1. Работа с типом int:");
        Box<int> intBox = new Box<int>();
        intBox.SetValue(42);
        int resultInt = intBox.GetValue();
        Console.WriteLine($"Полученное значение: {resultInt}");

        Console.WriteLine("\n2. Работа с типом string:");
        Box<string> stringBox = new Box<string>();
        stringBox.SetValue("Привет, мир!");
        string resultString = stringBox.GetValue();
        Console.WriteLine($"Полученное значение: {resultString}");

        Console.WriteLine("\n3. Работа с пользовательским классом Person:");
        Box<Person> personBox = new Box<Person>();

        Person person = new Person("Анна Иванова", 25);

        personBox.SetValue(person);

        Person resultPerson = personBox.GetValue();

        Console.WriteLine($"Полученный объект: {resultPerson}");

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
