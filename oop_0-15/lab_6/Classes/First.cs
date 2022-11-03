using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_6.Classes
{
    public class First
    {

        public First(int num)
        {
            if (num > 12)
            {
                throw new FirstException("Число не подходит!", num);
            }
            Number = num;
        }
        public First()
        {

        }

        public int Number
        {
            get => Number;
            set
            {
                if (value < 0)
                {
                    throw new FirstException("Отрицательные числа запрешены!");
                }
                else
                {
                    Number = value;
                }
            }
        }
    }
}