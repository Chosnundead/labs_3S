using System;
using lab_15.Classes;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите номер задания: ");
        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 1:
                Lab.task_1();
                break;
            case 2:
                Lab.task_2();
                break;
            case 3:
                Lab.task_3();
                break;
            case 4:
                Lab.task_4();
                break;
            case 5:
                Lab.task_5();
                break;
            case 6:
                Lab.task_6();
                break;
            case 7:
                Lab.task_7();
                break;
            case 8:
                Lab.task_8();
                break;
        }
    }
}