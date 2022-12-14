using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kr_2.Classes
{
    public class SuperLinkedList<T> : LinkedList<T>
    {
        public bool _Remove(T value)
        {
            if (!this.Contains(value))
            {
                throw new Exception("Ошибочка!");
            }
            return this.Remove(value);
        }
    }
}