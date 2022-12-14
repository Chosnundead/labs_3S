using System;
using lab_5.Classes;

class Program
{
    static void Main(string[] args)
    {
        Container container = new Container();
        container.addItem(new Human("Denis", "student", 21));
        container.addItem(new Human("Viktor", "student", 24));
        container.addItem(new Human("Vadim", "programmer", 44));
        container.addItem(new Human("Evgeniy", "military", 18));
        container.addItem(new Human("Nastya", "student", 21));
        container.showItems();

        container.task1();
        container.task2();
        container.task3();
        //перечесления
        //константы в перечеслении
        //структуры
    }
}