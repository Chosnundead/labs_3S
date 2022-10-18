using System;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Eat();
            person.Move();
            Time[] times = { new Time(23), new Time(21, 21, 21) };
            Console.WriteLine($"{times[0].h} : {times[0].getSec()} : {times[1].Equals(times[0])}");
            foreach (var item in times)
            {
                Console.WriteLine(item.ToString());
                if (item.h <= 12)
                {
                    Console.WriteLine(
                        "am"
                    );
                }
                else
                {
                    Console.WriteLine(
                        "pm"
                    );
                }
            }
            var time = new Time(2121);
            Console.WriteLine($"{time.h} : {time.getSec()} : {time.Equals(times[0])} : {time.getHashCode()} : {time.ToString()}");
            int numberForTest = 19;
            Console.WriteLine($"Before: {numberForTest}");
            time.test_0(ref numberForTest);
            Console.WriteLine($"Ref: {numberForTest}");
            time.test_1(out numberForTest);
            Console.WriteLine($"Out: {numberForTest}");
        }
    }
}
