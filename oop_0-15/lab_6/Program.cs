using System;
using System.Diagnostics;
using lab_6.Classes;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            try
            {
                First test = new First(21);
            }
            catch (FirstException ex)
            {
                Console.WriteLine($"value == {ex.Value}; {ex.Message}");
            }

            First one = new First();
            one.Number = -666;
        }
        catch (FirstException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

        try
        {
            Second two = new Second();
            two.Text = "qwertyasdfghzxcvbn";
        }
        catch (SecondException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        finally
        {
            Second twotest = new Second();
            twotest.Text = "2121.";
            try
            {
                twotest.placeDotAtEnd();
            }
            catch (SecondException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        try
        {
            Third three = new Third();
            try
            {
                Third.start();
            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex);
                foreach (var item in stackTrace.GetFrames())
                {
                    Console.WriteLine($"{item.GetMethod()}");
                }

            }
            three.IsTrue = false;
        }
        catch (ThirdException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        Second newSecond = new Second();//Программа прирывается и выводиться информация об ошибке(неверности условия)
        newSecond.Text = "2121";
    }
}