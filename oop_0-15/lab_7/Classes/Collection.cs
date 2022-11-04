using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;

namespace lab_7.Classes
{
    public class Collection<T> : ArrayList<T>
    {
        private LinkedList<T> arr;
        public Collection()
        {
            this.arr = new LinkedList<T>();
        }

        public void addFirstItem(T item)
        {

            this.arr.AddFirst(item);
        }

        public void removeLastItem()
        {
            try
            {
                this.arr.RemoveLast();
            }
            catch
            {
                Console.WriteLine($"Колекция не имеет элемента для удаления!");
            }
            finally
            {
                Console.WriteLine($"Debug: arr.Count == {this.arr.Count};");
            }
        }
        public T[] getItems()
        {
            return this.arr.ToArray<T>();
        }

        public void getFromFile()
        {
            string json = JsonSerializer.Serialize<Collection<T>>(this);
            Console.WriteLine(json);
        }
        public void writeToFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]));

            using (FileStream fs = new FileStream("collection.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, this.getItems());
            }
        }
        public T[] readFromFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]));

            T[]? arr;
            using (FileStream fs = new FileStream("collection.xml", FileMode.OpenOrCreate))
            {
                arr = xmlSerializer.Deserialize(fs) as T[];
            }
            return arr;
        }
    }
}