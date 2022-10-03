// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1a
            bool bool_val = true;
            byte byte_val = 5;
            sbyte sbyte_val = 5;
            char char_val = '5';
            decimal decimal_val = 5.0m;
            double double_val = 5.1d;
            float float_val = 5.2f;
            int int_val = 5;
            uint uint_val = 5;
            long long_val = 5;
            ulong ulong_val = 5;
            short short_val = 5;
            ushort ushort_val = 6;
            Console.WriteLine(String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}\n{10}\n{11}\n{12}", bool_val, byte_val, sbyte_val, char_val, decimal_val, double_val, float_val, int_val, uint_val, long_val, ulong_val, short_val, ushort_val));
            //1b
            //Неявное
            double_val = byte_val;
            double_val = sbyte_val;
            double_val = int_val;
            double_val = uint_val;
            double_val = Convert.ToDouble(long_val);
            //Явное
            int_val = (int)double_val;
            int_val = (int)float_val;
            int_val = (int)decimal_val;
            int_val = (int)long_val;
            int_val = Convert.ToInt32(ulong_val);
            //1c
            //Упаковка-распоковка
            object obj_val = int_val;
            int_val = (int)obj_val;
            //1d
            double_val = byte_val;
            double_val = 5;//Нет ошибки
            Console.WriteLine(double_val);
            //1e
            int? null_val = null;
            Console.WriteLine(null_val);
            //1f
            var test_val = 21;
            try
            {
                Console.WriteLine(test_val);
                test_val = (char)'r';
                Console.WriteLine(test_val);
            }
            catch
            {
                Console.WriteLine("Произошла ошибка переопределения переменной типа var!");
            }
            //2a
            string? str_0 = "test";
            string str_1 = "test";
            if (str_0 == str_1)
            {
                Console.WriteLine("Строковые литералы равны");
            }
            //2b
            String str_2 = new String("tezt");
            Console.WriteLine(String.Concat(str_0, str_1));
            str_0 = str_2;
            Console.WriteLine($"{str_0} == {str_2}");
            Console.WriteLine(str_0.IndexOf(str_1));
            str_0 = "test test test test";
            string[] words = str_0.Split(' ');
            foreach (var item in words)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(str_0.Insert(2, str_1));
            str_0.Replace("test", "");
            Console.WriteLine(str_0);
            Console.WriteLine($"{str_0} : {str_2}");
            //2c
            str_0 = null;
            str_1 = "";
            Console.WriteLine($"{string.IsNullOrEmpty(str_0)} <=> {string.IsNullOrEmpty(str_1)}");
            Console.WriteLine($"{str_0 + str_1}");
            //2d
            StringBuilder sb = new StringBuilder("HELLO!");
            sb.Remove(3, 1);
            Console.WriteLine($"{"test_" + sb + "_test"}");
            //3a
            int[,] arr = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    arr[i, j] = i * j;
                    Console.Write($"{arr[i, j]}\t");
                }
                Console.WriteLine();
            }
            //3b
            String?[] arr_words = new String[3] { "test", "tt", "es" };
            Console.Write($"index: ");
            int index = Convert.ToInt16(Console.ReadLine());
            Console.Write($"word: ");
            string? word = Console.ReadLine();
            try
            {
                arr_words[index] = word;
            }
            catch
            {

            }
            foreach (var item in arr_words)
            {
                Console.WriteLine(item);
            }
            //3c
            int[][] my_arr = { new int[] { 2, 1 }, new int[] { 2, 2, 2 }, new int[] { 5, 4, 3, 2 } };
            foreach (var arr_item in my_arr)
            {
                foreach (var item in arr_item)
                {
                    Console.Write($"{item}\t");
                }
                Console.WriteLine();
            }
            //3d
            var var_arr = new[] { 2, 3, 4 };
            var strokaaa = "ssstttrrroookkkaaa";
            //4a
            var tuple = (21, "21", '0', "21", 21);
            //4b
            Console.WriteLine(tuple);
            Console.WriteLine($"({tuple.Item1}, {tuple.Item3}, {tuple.Item4})");
            //4c
            int i1 = tuple.Item1;
            string i2 = tuple.Item2;
            char i3 = tuple.Item3;
            string i4 = tuple.Item4;
            ulong i5 = (ulong)tuple.Item5;
            //4d
            var tuple_ = (23, "23", '0', "23", 23);
            Console.WriteLine($"{tuple == tuple_}");
            //5
            (int max, int min, int sum, char first) func(int[] arr, string str)
            {
                return (arr.Max(), arr.Min(), arr.Sum(), str[0]);
            }
            int[] nums = { 1, 2, 3, 4 };
            Console.WriteLine(func(nums, "test"));
            //6
            int? local_func_0()
            {
                checked
                {
                    int num = 2147483647;
                }
                return null;
            }
            int? local_func_1()
            {
                unchecked
                {
                    int num = 2147483647;
                }
                return null;
            }
            Console.WriteLine(local_func_0());
            Console.WriteLine(local_func_1());
        }
    }
}
