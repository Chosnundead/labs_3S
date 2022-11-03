using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5.Classes
{
    public class Container
    {
        private List<Human> group = new List<Human>();

        public void addItem(Human item)
        {
            this.group.Add(item);
        }

        public void removeLast()
        {
            this.group.Remove(this.group.ToArray()[this.group.ToArray().Length - 1]);
        }

        public Human[] getItems()
        {
            return this.group.ToArray();
        }
        public void showItems()
        {
            foreach (var item in this.group)
            {
                Console.WriteLine($"name == {item.name}, age == {item.age}, profile == {item.profile}");
            }
        }


        public void task1()
        {
            int counter = 0;
            foreach (var item in this.group.ToArray())
            {
                if (item.profile == "student")
                {
                    counter += 1;
                }
            }
            Console.WriteLine($"Студентов == {counter}");
        }

        public void task2()
        {
            Human temp;
            Human[] toArray = this.group.ToArray();
            for (int i = 0; i < toArray.Length - 1; i++)
            {
                for (int j = i + 1; j < toArray.Length; j++)
                {
                    if (toArray[i].age > toArray[j].age)
                    {
                        temp = toArray[i];
                        toArray[i] = toArray[j];
                        toArray[j] = temp;
                    }
                }
            }
            foreach (var item in toArray)
            {
                Console.WriteLine($"name == {item.name}, age == {item.age}");
            }
            this.group = new List<Human>(toArray);
        }
        public void task3()
        {
            foreach (var item in this.group.ToArray())
            {
                if (item.profile == "programmer")
                {
                    Console.WriteLine($"Программист найден!");
                }
            }
        }


    }
}