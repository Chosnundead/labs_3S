using System;
using lab_14.Classes;
class Program
{
    static void Main(string[] args)
    {
        Lab.task_1();
        Lab.task_2();
        Console.Write("Выбирете задание(3 или 4): ");
        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 3:
                Lab.task_3();
                break;
            case 4:
                Lab.task_4();
                break;
        }
        Lab.task_5();
    }
}