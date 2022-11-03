using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_6.Classes
{
    public class Third
    {
        public bool IsTrue
        {
            get => IsTrue;
            set
            {
                if (value == false)
                {
                    throw new ThirdException("Не может содержать ложь!");
                }
                else
                {
                    IsTrue = value;
                }
            }
        }

        public static void one()
        {
            two();
        }
        public static void two()
        {
            three();
        }
        public static void three()
        {
            throw new ArgumentException();
        }
        public static void start()
        {
            one();
        }

    }
}