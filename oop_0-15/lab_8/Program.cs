using System;
using lab_8.Classes;

class Program
{
    public delegate void Message();
    public delegate void Games(string n1, string n2);
    public class MyEvent
    {
        public event Games? StartEvent;

        public void OnEvent()
        {
            Console.WriteLine($"Event! Game is starting!");
            this.StartEvent("Sven", "Mortred");
        }
    }

    static void Main(string[] args)
    {
        void gameStart(string nameOfWarrior, string nameOfAssassin)
        {
            Assassin assassin = new Assassin(nameOfAssassin, 1200.0, 10.0, 120.0, 0.5, 0.2);
            Warrior warrior = new Warrior(nameOfWarrior, 3000.0, 21.0, 150.0);
            int counter = 0;
            while (!(warrior.IsKilled) && !(assassin.IsKilled))
            {
                if (counter % 2 == 0)
                {
                    assassin.attack(ref warrior);
                }
                else
                {
                    warrior.attack(ref assassin);
                }
                counter += 1;
            }
            Console.WriteLine($"assassin hp == {assassin.Hp}\nwarrior hp == {warrior.Hp}");
        }

        var hello = () => Console.WriteLine("Hello!");//Пример лямбда выражения
        hello();

        Games games;
        MyEvent myEvent = new MyEvent();
        games = gameStart;
        games += gameStart;
        games += gameStart;
        myEvent.StartEvent += games;
        myEvent.OnEvent();

        Str str = new Str("S   tR  o Ka");
        str.editAllOne();
        Console.WriteLine(str.str);
        Console.WriteLine(str.editAllTwo(2, '3'));
        Console.WriteLine(str.str);
        // games("Sven", "Mortred");//Просто тройной запуск

        // int Add(int x, int y) => x + y;

        // int Multiply(int x, int y) => x * y;

        // void Hello() => Console.WriteLine("Hello METANIT.COM");
        // Message mes;            // 2. Создаем переменную делегата
        // mes = Hello;            // 3. Присваиваем этой переменной адрес метода
        // mes();                  // 4. Вызываем метод
        // Assassin assassin = new Assassin("Mortred", 1200.0, 10.0, 120.0, 0.5, 0.2);
        // Warrior warrior = new Warrior("Sven", 3000.0, 21.0, 150.0);
        // int counter = 0;
        // while (!(warrior.IsKilled) && !(assassin.IsKilled))
        // {
        //     if (counter % 2 == 0)
        //     {
        //         assassin.attack(ref warrior);
        //     }
        //     else
        //     {
        //         warrior.attack(ref assassin);
        //     }
        //     counter += 1;
        // }
        // Console.WriteLine($"assassin hp == {assassin.Hp}\nwarrior hp == {warrior.Hp}");
    }
}