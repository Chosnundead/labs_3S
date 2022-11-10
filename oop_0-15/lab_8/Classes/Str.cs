using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_8.Classes
{
    public class Str
    {
        public delegate void Action();
        public delegate T Func<out T, in T1, in T2>(T1 arg1, T2 arg2);
        public string str;
        public Str(string str)
        {
            this.str = str;
        }

        public void deleteAllDots()
        {
            string newStr = "";
            foreach (var item in this.str)
            {
                if (item != '.')
                {
                    newStr += item;
                }
            }
            this.str = newStr;
        }

        public string addSymbols(int amount, char symbol)
        {
            string result = "";
            string newStr = this.str;
            for (int i = 0; i < amount; i++)
            {
                newStr += symbol;
                result += symbol;
            }
            this.str = newStr;
            return result;
        }

        public void toUpperCase()
        {
            string newStr = this.str;
            this.str = newStr.ToUpper();
        }

        public void deleteUselessSpaces()
        {
            string newStr = "";
            for (int i = 0; i < this.str.Length; i++)
            {
                try
                {
                    if (this.str[i] == ' ' && this.str[i] == this.str[i - 1])
                    {

                    }
                    else
                    {
                        newStr += this.str[i];
                    }
                }
                catch (Exception ex)
                {
                    //Заглушка на ощибку
                }
            }
            this.str = newStr;
        }

        public void editAllOne()
        {
            Console.WriteLine("deleteUselessSpaces + toUpperCase");
            Action action;
            action = deleteUselessSpaces;
            action += toUpperCase;
            action();
        }

        public string editAllTwo(int amount, char symbol)
        {
            Console.WriteLine("addSymbols");
            Func<string, int, char> func;
            func = addSymbols;
            return func(amount, symbol);
        }
    }
}