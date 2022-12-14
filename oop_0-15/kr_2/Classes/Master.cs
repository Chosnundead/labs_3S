using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kr_2.Classes
{
    public class Master
    {
        public delegate void Response();
        public event Response onUp;
        public void up()
        {
            Console.WriteLine("master says: \"up!\"");
            onUp();
        }
    }
}