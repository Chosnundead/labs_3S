using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_6.Classes
{
    public class FirstException : Exception
    {
        public int Value { get; set; }
        public FirstException(string message, int value)
        : base(message)
        {
            Value = value;
        }
        public FirstException(string message)
        : base(message)
        {

        }
    }
}