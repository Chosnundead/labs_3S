using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


static public class Orator
{
    public static void Checker(People pe)
    {
        ICan check;
        if (pe is ICan)
        {
            check = (ICan)pe;
            Console.WriteLine("Да поддерживает!");
            check.Speak();
        }
    }
}
