using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_8.Classes
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public double Hp { get; set; }
        public double Defense { get; set; }

        public GameObject() { }

        public GameObject(string name, double hp, double defense)
        {
            Console.WriteLine($"{name} was created!");
            this.Name = name;
            this.Hp = hp;
            this.Defense = defense;
        }
    }
}