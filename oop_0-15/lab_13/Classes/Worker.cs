using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_13.Classes
{
    [Serializable]
    abstract public class Worker : Person
    {
        public Worker()
        {
            Console.WriteLine("Worker class created!");
        }
        public string name;
        public string? workPlace;
        public virtual string getName() { return this.name; }
        public virtual void _setName(string name) { this.name = name; }
        public abstract string getWorkPlace();
    }
}