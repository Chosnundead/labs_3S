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
            //Упаковка-распоковка
            object obj_val = int_val;
            int_val = (int)obj_val;
            var test_val = 5f / 5 * 5.2d;
            test_val = 5;//Нет ошибки
            Console.WriteLine(test_val);
            int? null_val = null;
            Console.WriteLine(null_val);
            Console.ReadKey();
            string string_val = $"{test_val} exmple str{null_val}ing\n";

        }
    }
}
