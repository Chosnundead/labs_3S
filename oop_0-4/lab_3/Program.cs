using System;

namespace StrClass
{
    public class Program
    {
        static void Main(string[] args)
        {
            Str stroka = new Str("tesdwxt");
            Console.WriteLine("test:");
            Console.WriteLine(stroka * 'b');
            Console.WriteLine(stroka > new Str("ss"));
            Console.WriteLine(stroka < new Str("ss"));
            Console.WriteLine(stroka + 5);
            Console.WriteLine((stroka--).text);
            Console.WriteLine((--stroka).text);
            Console.WriteLine();
            stroka.production.id = 2;
            stroka.production.name = "University";
            Console.WriteLine($"id: {stroka.production.id}\tnameOfOrganization: {stroka.production.name}");
            stroka.developer.id = 0;
            stroka.developer.state = 21;
            stroka.developer.name = "Denis Solodkiy Vicktorovich";
            Console.WriteLine($"id: {stroka.developer.id}\tfullNameOfDev: {stroka.developer.name}\tstate: {stroka.developer.state}");
            Console.WriteLine($"\ntask4:\nSum:{StrExtension.FunnySum(stroka)}");
            Console.WriteLine($"Len:{StrExtension.Len(stroka)}");
            Console.WriteLine($"MinMax:({StrExtension.MinMax(stroka)[0]}, {StrExtension.MinMax(stroka)[1]})");
            Console.WriteLine(stroka.Len());
        }
    }
}

