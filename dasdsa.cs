using System;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }

    private static int studentCount = 0;

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
        studentCount++;
    }

    public static int GetStudentCount()
    {
        return studentCount;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Создание студентов:\n");

        Student student1 = new Student("Илья", 20);
        student1.PrintInfo();

        Student student2 = new Student("Иван", 19);
        student2.PrintInfo();

        Student student3 = new Student("Вадим", 21);
        student3.PrintInfo();

        Student student4 = new Student("Леонид", 18);
        student4.PrintInfo();

        Console.WriteLine("\n---");
        Console.WriteLine($"Общее количество созданных студентов: {Student.GetStudentCount()}");
    }
}
