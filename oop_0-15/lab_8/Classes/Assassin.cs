using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_8.Classes
{
    public class Assassin : PersonObject
    {
        public double Dodge { get; set; }
        public double Critical { get; set; }
        public Assassin(string name, double hp, double defense, double damage, double dodge, double critical)
        {
            Console.WriteLine($"{name} was created!");
            this.Name = name;
            this.Hp = hp;
            this.Defense = defense;
            this.Damage = damage;
            this.Dodge = dodge;
            this.Critical = critical;
        }

        public void attack(ref Warrior obj)
        {
            // Console.WriteLine("debug. Attack in assassin class");
            Random random = new Random();
            double dmg;
            if (random.NextDouble() <= this.Critical)
            {
                dmg = this.Damage * 2 - obj.defense(this.Damage);
            }
            else
            {
                dmg = this.Damage - obj.defense(this.Damage);
            }
            obj.Hp -= dmg;
            if (obj.IsKilled)
            {
                Console.WriteLine($"{obj.Name} was killed!");
            }
        }

        public new double defense(double damage)
        {
            Random random = new Random();
            if (random.NextDouble() <= this.Dodge)
            {
                return damage;
            }
            else
            {
                return Math.Sqrt(this.Defense);
            }
        }
    }
}