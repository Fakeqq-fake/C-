using System;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book()
    {
        Title = "Неизвестно";
        Author = "Неизвестен";
        Year = 0;
    }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Название: {Title}");
        Console.WriteLine($"Автор: {Author}");
        Console.WriteLine($"Год издания: {Year}");
        Console.WriteLine("---");
    }

    public bool IsOld()
    {
        int currentYear = DateTime.Now.Year;
        return (currentYear - Year) > 20;
    }
}

class Program
{
    static void Main()
    {
        Book book1 = new Book();
        Console.WriteLine("Книга 1 (конструктор без параметров):");
        book1.PrintInfo();
        Console.WriteLine($"Книга старая: {book1.IsOld()}");
        Console.WriteLine();

        Book book2 = new Book("Мастер и Маргарита", "Михаил Булгаков", 1966);
        Console.WriteLine("Книга 2 (конструктор с параметрами):");
        book2.PrintInfo();
        Console.WriteLine($"Книга старая: {book2.IsOld()}");
        Console.WriteLine();

        Book book3 = new Book
        {
            Title = "1984",
            Author = "Джордж Оруэлл",
            Year = 1949
        };
        Console.WriteLine("Книга 3 (инициализатор объектов):");
        book3.PrintInfo();
        Console.WriteLine($"Книга старая: {book3.IsOld()}");
        Console.WriteLine();

        Book book4 = new Book("Война и мир", "Лев Толстой", 1869)
        {
            Title = "Война и мир (полное издание)"
        };
        Console.WriteLine("Книга 4 (комбинация конструктора и инициализатора):");
        book4.PrintInfo();
        Console.WriteLine($"Книга старая: {book4.IsOld()}");
    }
}
