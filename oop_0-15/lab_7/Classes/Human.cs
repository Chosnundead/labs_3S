using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_7.Classes
{
    public class Human
    {
        public string Name { get; set; }

        private int age;

        public void becomeOlder()
        {
            this.age += 1;
        }

        public int getAge()
        {
            return this.age;
        }

        public Human(string name, int age)
        {
            this.Name = name;
            this.age = age;
        }

        public Human()
        {

        }
    }
}