using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5.Classes
{
    public partial class Human : IComparable
    {
        public string name;

        public static int count = 0;

        public int CompareTo(object? obj)
        {
            if (obj is Human human) return name.CompareTo(human.name);
            else throw new ArgumentException("Некорректное значение параметра");
        }

        public Human(string name, string profile, int age)
        {
            count += 1;
            this.name = name;
            this.profile = profile;
            this.age = age;
        }
    }
}