using System;

namespace kr_1
{

    class Program
    {
        static void Main(string[] args)
        {
            string task1 = "testWordForTest";
            int len = 3;
            int fromPos = 5;
            string newString = "";

            foreach (var item in task1)
            {
                if (len > 0 && fromPos <= 0)
                {
                    newString += item;
                    len--;
                }
                fromPos--;
            }
            Console.WriteLine(newString);

            List<string> stringArr = new List<string>();
            stringArr.Add("test1");
            stringArr.Add("test2");
            stringArr.Add("test3");
            foreach (var item in stringArr)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"{((new Date(2, 5, 2001).GetHashCode() == new Date(21, 6, 2010).GetHashCode()) ? true : false)}");

            People pe = new People();
            Orator.Checker(pe);
        }
    }
}