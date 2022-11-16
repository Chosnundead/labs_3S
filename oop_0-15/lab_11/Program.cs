using System;
using System.Reflection;
using lab_11.Classes;
class Program
{
    public static void showResults(string? result)
    {
        Console.WriteLine($"{result}");
    }
    public static void showResults(string? result, string title)
    {
        Console.WriteLine($"\n\t{title}\n{result}");
    }
    public static void showResults(string? result, string title, string description)
    {
        Console.WriteLine($"\n\t{title}\n{result}\n{description}");
    }
    static void Main(string[] args)
    {
        showResults(Reflector.getAssemblyName("lab_11.Classes.Reflector"), "task_1a:");
        showResults(Reflector.isPublicCtors("System.Int32").ToString() + "\n", "task_1b:");
        showResults(Reflector.getPublicMethods("System.Double"), "task_1c:");
        showResults(Reflector.getFieldsAndProps("System.String"), "task_1d:");
        showResults(Reflector.getInterfaces("System.Char"), "task_1e:");
        showResults(Reflector.getMethodsByParams("lab_11.Classes.Time", "int"), "task_1f:");
        Console.WriteLine($"\n\ttask_1g:");
        Reflector.Invoke(new Time(2121), "ttt", new object[] { 2 });
        Console.WriteLine($"\n\n\ttask_2:");
        Reflector.Create("lab_11.Classes.Time");
    }
}