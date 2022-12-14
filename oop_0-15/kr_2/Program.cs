using System;
using kr_2.Classes;
class Program
{
    static void Main(string[] args)
    {
        //Вариант#3
        //task_1
        SuperLinkedList<string> strings = new SuperLinkedList<string>();
        strings.AddFirst("te_1");
        strings.AddFirst("tet_2");
        strings.AddFirst("test_3");
        strings.AddFirst("test__4");
        try
        {
            strings._Remove("test");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Была ошибка: {ex.Message}");
        }
        //task_2
        var result = from item in strings
                     where (item.Length < 6)
                     select item;
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
        //task_3
        Master master = new Master();
        Horse horse_1 = new Horse();
        Horse horse_2 = new Horse();
        master.onUp += horse_1.responseOnUp;
        master.onUp += horse_2.responseOnUp;
        master.up();
    }
}