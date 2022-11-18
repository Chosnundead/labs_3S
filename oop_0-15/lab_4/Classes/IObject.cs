using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Classes
{
    public abstract class IObject
    {
        public void IAmPrinting(IObject obj)
        {
            Console.WriteLine("test!");
            Console.WriteLine("test!");
        }
    }
}