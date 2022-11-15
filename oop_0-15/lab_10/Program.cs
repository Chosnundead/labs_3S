using System;
using System.Collections.Generic;
using lab_10.Classes;
using System.Linq;

class Program
{
    static void show(string title, IEnumerable<string> arr)
    {
        Console.WriteLine($"\n{title}");
        foreach (var item in arr)
        {
            Console.WriteLine(item);
        }
    }
    static void show(string title, IEnumerable<Time> arr)
    {
        Console.WriteLine($"\n{title}");
        foreach (var item in arr)
        {
            Console.WriteLine($"year: {item.year}; month: {item.month}; day: {item.day}; hour: {item.h}; minute: {item.min}; second: {item.min}");
        }
    }
    static void show(string title, IEnumerable<IGrouping<int, Time>> arr)
    {
        Console.WriteLine($"\n{title}");
        foreach (var item in arr)
        {
            Console.WriteLine($"{item.Key}:");
            foreach (var time in item)
            {
                Console.WriteLine($"year: {time.year}; month: {time.month}; day: {time.day}; hour: {time.h}; minute: {time.min}; second: {time.min}");
            }
        }
    }
    static void Main(string[] args)
    {
        string[] strings = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        int n = 4;
        var result_1 = from item in strings
                       where item.Length == n
                       select item;
        var result_2 = from item in strings
                       where (new string[] { "January", "February", "June", "July", "August", "December" }.Contains(item))
                       select item;
        var result_3 = from item in strings
                       orderby item
                       select item;
        var result_4 = from item in strings
                       where (item.Length >= 4 && item.Contains('u'))
                       select item;
        show("запрос выбирающий последовательность месяцев с длиной строки равной n", result_1);
        show("запрос возвращающий только летние и зимние месяцы", result_2);
        show("запрос вывода месяцев в алфавитном порядке", result_3);
        show("запрос считающий месяцы содержащие букву «u» и длиной имени не менее 4-х", result_4);

        var times = new List<Time>(new Time[] { new Time(1234), new Time(1, 23), new Time(4321), new Time(21, 2, 3), new Time(2211), new Time(5432), new Time(21, 21, 21), new Time(1734), new Time(7, 23), new Time(7321) });
        int year = 2;
        var result_5 = from item in times
                       where item.year == year
                       select item;
        show("список дат для заданного года", result_5);
        int month = 2;
        var result_6 = from item in times
                       where item.month == month
                       select item;
        show("список дат, которые имеют заданный месяц", result_6);
        var result_7 = from item in times
                       where ((item.year > 2) && (item.h <= 21) && (item.month != 8))
                       select item;
        show("количество дат в определённом диапазоне", result_7);
        var result_8 = from item in times
                       orderby item.year
                       select item;
        var time = result_8.Last();
        Console.WriteLine($"\nмаксимальную дату");
        Console.WriteLine($"year: {time.year}; month: {time.month}; day: {time.day}; hour: {time.h}; minute: {time.min}; second: {time.min}");
        int day = 21;
        var result_9 = from item in times
                       where item.day == day
                       select item;
        Console.WriteLine($"\nПервую дату для заданного дня");
        try
        {
            time = result_9.First();
            Console.WriteLine($"year: {time.year}; month: {time.month}; day: {time.day}; hour: {time.h}; minute: {time.min}; second: {time.min}");
        }
        catch (Exception e) { }
        var result_10 = from item in times
                        orderby item.getFullSeconds()
                        select item;
        show("Упорядоченный список дат (хронологически)", result_10);
        var joinArr = new List<Date>(new Date[] { new Date(6, "Hello World!"), new Date(2, "World Hello!") });
        var result_11 = from item in times
                        join joinItem in joinArr on item.year equals joinItem.year
                        select new { year = item.year, day = item.day, ev = joinItem.ev };
        Console.WriteLine($"\nMy_1");
        foreach (var item in result_11)
        {
            Console.WriteLine($"year: {item.year}; day: {item.day}; yearEvent: {item.ev}");
        }
        var result_12 = from item in times
                        orderby item.getFullSeconds() descending
                        select item;
        show("My_2", result_12);
        var result_13 = from item in times
                        where ((item.year != 2) && (item.h >= 21))
                        select item;
        show("My_3", result_13);
        var result_14 = from item in times
                        group item by item.year;
        show("My_4", result_14);
        var result_15 = from item in times
                        group item by item.day;
        show("My_5", result_15);
    }
}