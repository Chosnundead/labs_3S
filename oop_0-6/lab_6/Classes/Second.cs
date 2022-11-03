using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab_6.Classes
{
    public class Second
    {
        private string text;
        public string Text
        {
            get => text;
            set
            {
                if (value.Length > 10)
                {
                    throw new SecondException("Строка с более чем 10 символами недопустима!");
                }
                Debug.Assert(value != "2121");
                text = value;
            }
        }

        public void placeDotAtEnd()
        {

            string str = Text;
            if (str.Last().Equals('.'))
            {
                throw new SecondException("В конце уже находиться точка!");
            }
            str = $"{str}.";
            Text = str;
        }
    }
}