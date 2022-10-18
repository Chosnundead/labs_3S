using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// static public class StaticOperations
static public class StrExtension
{
    static public string FunnySum(this Str a)
    {
        string result = "";
        foreach (var item in a.text)
        {
            result += $"{item}+";
        }
        return result;
    }
    static public int Len(this Str a)
    {
        return a.text.Length;
    }
    static public char[] MinMax(this Str a)
    {
        int min = (int)a.text[0], max = (int)a.text[0];
        foreach (var item in a.text)
        {
            int number = (int)item;
            if (number > max)
            {
                max = number;
            }
            if (number < min)
            {
                min = number;
            }
        }
        char charmin = (char)min;
        char charmax = (char)max;
        char[] arr = new char[] { charmin, charmax };
        return arr;
    }
}

public class Str
{
    public class Production
    {
        public int? id;
        public string? name;
    }
    public Production production = new Production();
    public class Developer
    {
        public int? id;
        public int? state;
        public string? name;
    }
    public Developer developer = new Developer();
    public string text;
    private char[] Force = { '$', '=', '\\', '/', '~' };
    private char[] Spaces = { ' ', ',', '.', ';' };

    public Str(string str)
    {
        this.text = str;
    }

    public bool checkForForce()
    {
        bool isContain = false;

        foreach (var item in this.Force)
        {
            if (this.text.Contains(item))
            {
                isContain = true;
                break;
            }
        }

        return isContain;
    }

    public string deleteSpaces()
    {
        foreach (var item in this.Spaces)
        {
            while (this.text.IndexOf(item) != -1)
            {
                this.text.Remove(this.text.IndexOf(item), 1);
            }
        }

        return this.text;
    }

    public static bool operator >(Str a, Str b)
    {
        return a.text.Length > b.text.Length;
    }

    public static bool operator <(Str a, Str b)
    {
        return a.text.Length < b.text.Length;
    }

    public static string operator +(Str a, int b)
    {
        a.text = $"{a.text}{b}";
        return a.text;
    }

    public static Str operator --(Str a)
    {
        if (a.text.Length != 0)
        {
            a.text = a.text.Substring(0, a.text.Length - 1);
        }

        return new Str(a.text);
    }

    public static string operator *(Str a, char b)
    {
        string result = "";
        for (int step = 0; step < a.text.Length; step++)
        {
            result += b;
        }
        a.text = result;
        return a.text;
    }
}
