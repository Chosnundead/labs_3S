using System;

namespace lab_4.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            Military miForRef = new Military("Denis");
            Student stForRef = new Student("Ilya");
            Learner st = stForRef as Learner;
            Worker mi = miForRef as Worker;
            Turner tu = new Turner("Vladick");
            ExtramuralStudent ex = new ExtramuralStudent("Danik");
            //конструктор в абстрактном классе
            Programmer pr = new Programmer("Egor");

            Console.WriteLine(mi.ToString());
            Console.WriteLine(tu.ToString());
            Console.WriteLine(st.ToString());
            Console.WriteLine(ex.ToString());
            Console.WriteLine(pr.ToString());
            Console.WriteLine($"pr.Equals(ex) == {pr.Equals(ex)}; pr.ToString() == \"{pr.ToString()}\"; pr.GetHashCode() == {pr.GetHashCode()};");

            Printer printer = new Printer();
            IObject iobject = printer as Printer;

            Console.WriteLine();

            //одноименные методы
            printer.IAmPrinting(printer);
            printer.IAmPrinting(printer, "test");

            Console.WriteLine();
            iobject.IAmPrinting(iobject);
            //конструктор в абстрактном классе, одноименные методы
        }
    }
}