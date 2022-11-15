using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace lab_9.Classes
{
    public class Computer
    {
        private string model;
        public string Model { get; }

        public Computer(string model)
        {
            this.model = model;
        }

        public void print(string text)
        {
            Console.WriteLine($"Computer {this.model}: {text}");
        }
        public override bool Equals(object obj) => this.Equals(obj as Computer);

        public bool Equals(Computer computer)
        {
            //??? ШО ТУТ ПРОИСХОДИТ?
            return this.model.Equals(computer.model);
        }
    }
    public static class Task3
    {
        public static void Start()
        {
            ObservableCollection<Computer> collection = new ObservableCollection<Computer>();
            void onChange(object? sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Console.WriteLine("Item added!");
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Console.WriteLine("Item removed!");
                        break;
                }
            }
            collection.CollectionChanged += onChange;
            collection.Add(new Computer("Lenovo Legion"));
            collection.Add(new Computer("Asus ROG"));
            collection.RemoveAt(1);
        }
    }
}