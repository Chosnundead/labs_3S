using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class People : ICan
{
    public void Speak()
    {
        Console.WriteLine("Speaking!");
    }
}
