using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class Date
{
    public int day, mounth, year;

    public Date(int day, int mounth, int year)
    {
        this.day = day;
        this.mounth = mounth;
        this.year = year;
    }

    public override string ToString()
    {
        return $"{this.day}.{this.mounth}.{this.year}";
    }

    public override int GetHashCode()
    {
        return this.day * 1 + this.mounth * 100 + this.year * 10000;
    }
}
public partial class Date
{
    public bool isCorrect()
    {
        return (day >= 31 && day <= 1 && mounth >= 1 && mounth <= 12 && year >= 1) ? true : false;
    }
}
