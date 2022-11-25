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
    public static void writeInLog(string? str)
    {
        File.AppendAllText(@"Files\logfile.txt", str);
    }
    static void Main(string[] args)
    {
        File.WriteAllText(@"Files\logfile.txt", "");
        showResults(Reflector.getAssemblyName("lab_11.Classes.Reflector"), "task_1a:");
        writeInLog(Reflector.getAssemblyName("lab_11.Classes.Reflector"));
        showResults(Reflector.isPublicCtors("System.Int32").ToString() + "\n", "task_1b:");
        writeInLog(Reflector.isPublicCtors("System.Int32").ToString());
        showResults(Reflector.getPublicMethods("System.Double"), "task_1c:");
        writeInLog(Reflector.getPublicMethods("System.Double"));
        showResults(Reflector.getFieldsAndProps("System.String"), "task_1d:");
        writeInLog(Reflector.getFieldsAndProps("System.String"));
        showResults(Reflector.getInterfaces("System.Char"), "task_1e:");
        writeInLog(Reflector.getInterfaces("System.Char"));
        showResults(Reflector.getMethodsByParams("lab_11.Classes.Time", "int"), "task_1f:");
        writeInLog(Reflector.getMethodsByParams("lab_11.Classes.Time", "int"));
        Console.WriteLine($"\n\ttask_1g:");
        var task1g = File.ReadAllLines(@"Files\task1g.txt");
        Reflector.Invoke(Reflector.Create(task1g[0]), task1g[1], new object[] { new Random().Next(1, 666) });
        Console.WriteLine($"\n\n\ttask_2:");
        Reflector.Create("lab_11.Classes.Time");
    }
}