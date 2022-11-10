using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_8.Classes
{
    public class Warrior : PersonObject
    {
        public Warrior(string name, double hp, double defense, double damage)
        {
            Console.WriteLine($"{name} was created!");
            this.Name = name;
            this.Hp = hp;
            this.Defense = defense;
            this.Damage = damage;
        }
        public void attack(ref Assassin obj)
        {
            double dmg = this.Damage - obj.defense(this.Damage);
            obj.Hp -= dmg;
            if (obj.IsKilled)
            {
                Console.WriteLine($"{obj.Name} was killed!");
            }
        }
    }
}