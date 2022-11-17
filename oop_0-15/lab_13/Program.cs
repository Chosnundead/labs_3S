using System;
using lab_13.Classes;
using System.Runtime.Serialization.Formatters.Binary;
// using System.Runtime.Serialization.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        var military = new Military("Denis");
        //binary
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (var fs = new FileStream(@"Files\binary.dat", FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fs, military);
        }
        using (var fs = new FileStream(@"Files\binary.dat", FileMode.OpenOrCreate))
        {
            var newMilitary = (Military)binaryFormatter.Deserialize(fs);
            Console.WriteLine($"{newMilitary.ToString()}");
            Console.WriteLine($"Поле с запрещённой сериализацией test == {newMilitary.test}");
        }
        //soap
        //dotnet add package System.Runtime.Serialization.Formatters
        //и
        //dotnet add package System.Runtime.Serialization.Formatters.Soap
        //не добавляют этот пакет -> невозможно сделать эту чать лабы
        // SoapFormatter soapFormatter = new SoapFormatter();
        // using (var fs = new FileStream(@"Files\soap.soap", FileMode.OpenOrCreate))
        // {
        //     soapFormatter.Serialize(fs, military);
        // }
        // using (var fs = new FileStream(@"Files\soap.soap", FileMode.OpenOrCreate))
        // {
        //     var newMilitary = (Military)soapFormatter.Deserialize(fs);
        //     Console.WriteLine($"{newMilitary.ToString()}");
        //     Console.WriteLine($"Поле с запрещённой сериализацией test == {newMilitary.test}");
        // }
        //json
        var jsonFormatter = new DataContractJsonSerializer(typeof(Military));
        using (var fs = new FileStream(@"Files\json.json", FileMode.OpenOrCreate))
        {
            jsonFormatter.WriteObject(fs, military);
        }
        using (var fs = new FileStream(@"Files\json.json", FileMode.OpenOrCreate))
        {
            var newMilitary = (Military)jsonFormatter.ReadObject(fs);
            Console.WriteLine($"{newMilitary.ToString()}");
        }
        //xml
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Military));
        using (var fs = new FileStream(@"Files\xml.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, military);
        }
        using (var fs = new FileStream(@"Files\xml.xml", FileMode.OpenOrCreate))
        {
            var newMilitary = (Military)xmlSerializer.Deserialize(fs);
            Console.WriteLine($"{newMilitary.ToString()}");
        }
        //collection
        var arr = new Military[] { new Military("Denis"), new Military("Vlad") };
        BinaryFormatter binaryArrayFormatter = new BinaryFormatter();
        using (var fs = new FileStream(@"Files\binary.dat", FileMode.OpenOrCreate))
        {
            binaryArrayFormatter.Serialize(fs, arr);
        }
        using (var fs = new FileStream(@"Files\binary.dat", FileMode.OpenOrCreate))
        {
            var newMilitaryArr = (Military[])binaryArrayFormatter.Deserialize(fs);
            foreach (var item in newMilitaryArr)
            {
                Console.WriteLine($"{item.ToString()}");
            }
        }
        //xpath
        var xDoc = new XmlDocument();
        xDoc.Load(@"Files\xml.xml");
        Console.WriteLine("Вывод всей иерархии:");
        foreach (XmlNode item in xDoc.DocumentElement.SelectNodes("*"))
        {
            Console.WriteLine(item.OuterXml);
        }
        Console.WriteLine("Вывод только имен:");
        foreach (XmlNode item in xDoc.DocumentElement.SelectNodes("name"))
        {
            Console.WriteLine(item.OuterXml);
        }
        //linq-to-xml
        var xDocMain = XDocument.Load(@"Files\xmlForLinqTest.xml");
        // xDocMain.Load(@"Files\xmlForLinqTest.xml");
        Console.WriteLine("Вывод Вадимов:");
        var result_1 = xDocMain.Element("Root").Elements("person").Where(p => p.Element("name").Value == "Vadim").Select(p => new { age = p.Element("age").Value, workPlace = p.Element("workPlace").Value });
        foreach (var item in result_1)
        {
            Console.WriteLine($"age: {item.age}\\workPlace: {item.workPlace}");
        }
        Console.WriteLine("Вывод Возрастов:");
        var result_2 = xDocMain.Element("Root").Elements("person").Select(p => p.Element("age").Value);
        foreach (var item in result_2)
        {
            Console.WriteLine(item);
        }
    }
}