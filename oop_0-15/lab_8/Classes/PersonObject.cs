using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_8.Classes
{
    public abstract class PersonObject : GameObject
    {
        public bool IsKilled { get; set; } = false;
        private double hp;
        public new double Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    IsKilled = true;
                }
            }
        }
        public double Damage { get; set; }
        public void attack(ref PersonObject obj)
        {
            double dmg = this.Damage - obj.defense(this.Damage);
            obj.Hp -= dmg;
            if (obj.IsKilled)
            {
                Console.WriteLine($"{obj.Name} was killed!");
            }
        }

        public double defense(double damage)
        {
            return Math.Sqrt(this.Defense);
        }
        public PersonObject() { }
        public PersonObject(string name, double hp, double defense, double damage) : base(name, hp, defense)
        {
            this.Damage = damage;
        }
    }
}