using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Classes
{
    public class Printer : IObject
    {
        public Student student;
        public void IAmPrinting(IObject obj)
        {
            Console.WriteLine(Convert.ToString(obj.GetType()));
            Console.WriteLine(obj.ToString());
        }
        public void IAmPrinting(IObject obj, string param)
        {
            Console.WriteLine(Convert.ToString(obj.GetType()));
            Console.WriteLine($"{obj.ToString()}{param}");
        }
    }
}