using System;
using lab_9.Classes;

class Program
{
    public static void printFullCollectionOfComputers(Collection<Computer> computers)
    {
        foreach (var item in computers)
        {
            item.print("Hello World!");
        }
    }

    public static void Delete(ref Collection<Computer> param, Computer computer)
    {
        Collection<Computer> newCollection = new Collection<Computer>();
        foreach (var item in param)
        {
            if (!computer.Equals(item))
            {
                newCollection.Add(item);
            }
        }
        param = newCollection;
    }

    public static void Search(Collection<Computer> computers, Computer computer)
    {
        foreach (var item in computers)
        {
            if (item.Equals(computer))
            {
                item.print("This is what you are searching!");
            }
        }
    }
    static void Main(string[] args)
    {
        //task 1;
        Collection<Computer> computers = new Collection<Computer>();
        computers.Add(new Computer("Lenovo Legion"));
        computers.Add(new Computer("Asus ROG"));
        computers.Add(new Computer("Mac M1"));
        Console.WriteLine("\nДобваление:");
        printFullCollectionOfComputers(computers);
        Console.WriteLine("\nУдаление:");
        Delete(ref computers, new Computer("Lenovo Legion"));
        printFullCollectionOfComputers(computers);
        Console.WriteLine("\nПоиск:");
        Search(computers, new Computer("Mac M1"));
        Console.WriteLine("\nВывод:");
        printFullCollectionOfComputers(computers);

        //task 2;
        HashSet<char> chars = new HashSet<char>(new char[] { 'H', 'e', 'l', 'l', 'o', ':', ')' });
        Console.WriteLine("\na)");
        foreach (var item in chars)
        {
            Console.Write(item);
        }
        Console.WriteLine("\nb)");
        int counter = 0;
        foreach (var item in chars)
        {
            chars.Remove(item);
            if (counter >= 1)
            {
                break;
            }
            counter += 1;
        }
        foreach (var item in chars)
        {
            Console.Write(item);
        }
        Console.WriteLine("\nc)");
        chars.Add('c');
        chars.Add('e');
        foreach (var item in chars)
        {
            Console.Write(item);
        }
        Console.WriteLine("\nd and e)");
        LinkedList<char> chars1 = new LinkedList<char>(chars.ToArray<char>());
        foreach (var item in chars1)
        {
            Console.Write(item);
        }
        Console.WriteLine("\nf)");
        Console.WriteLine(chars1.Find('o').Value);

        //task 3;
        Task3.Start();

        // bool test = computers.Remove(new Computer("Lenovo Legeon"));
        // Console.WriteLine($"{test}");
    }
}
