using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_6.Classes
{
    public class ThirdException : SecondException
    {
        public ThirdException(string message)
        : base(message)
        { }
    }
}