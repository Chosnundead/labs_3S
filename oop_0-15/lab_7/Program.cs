using System;
using lab_7.Classes;
using System.Xml.Serialization;
class Program
{
    static void Main(string[] args)
    {
        Collection<int> arrInt = new Collection<int>();
        //Поверка с int
        arrInt.addFirstItem(6);
        arrInt.addFirstItem(5);
        arrInt.addFirstItem(6);
        arrInt.addFirstItem(6);
        arrInt.removeLastItem();
        arrInt.removeLastItem();
        arrInt.removeLastItem();
        arrInt.removeLastItem();
        arrInt.removeLastItem();
        foreach (var item in arrInt.getItems())
        {
            Console.WriteLine($"{item};");
        }

        Collection<double> arrDouble = new Collection<double>();
        arrDouble.addFirstItem(2.5);
        arrDouble.addFirstItem(2.6);
        arrDouble.addFirstItem(2.7);
        foreach (var item in arrDouble.getItems())
        {
            Console.WriteLine($"{item};");
        }

        Collection<Human> arrHuman = new Collection<Human>();
        arrHuman.addFirstItem(new Human("Denis", 21));
        arrHuman.addFirstItem(new Human("Vadim", 19));
        arrHuman.addFirstItem(new Human("Jenya", 23));

        foreach (var item in arrHuman.getItems())
        {
            Console.WriteLine($"{item.Name}, {item.getAge()} years old;");
        }
        writeToFileArrHuman(arrHuman.getItems());
        foreach (var item in readFromFileArrHuman())
        {
            Console.WriteLine($"{item.Name}, {item.getAge()} years old;//Ноль выводиться т.к. поле с возрастом - приватное");
        }

    }
    static public void writeToFileArrHuman(Human[] arr)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Human[]));

        using (FileStream fs = new FileStream("arrHuman.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, arr);
            // Console.WriteLine("Object has been serialized");
        }
    }
    static public Human[] readFromFileArrHuman()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Human[]));

        Human[]? arr;
        using (FileStream fs = new FileStream("arrHuman.xml", FileMode.OpenOrCreate))
        {
            arr = xmlSerializer.Deserialize(fs) as Human[];
        }
        return arr;
    }
}