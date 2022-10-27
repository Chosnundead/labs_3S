using System;

public class Time
{
    public const int max_hours = 24;
    public int h;
    public int min;
    public int sec;
    static string defaultFormat = "24h";
    private string timeFormat = "24h";
    static int numberOfObjects = 0;


    public bool Equals(Time time)
    {
        return (time.h == this.h && time.min == this.min && time.sec == this.sec) ? true : false;
    }
    public override string ToString()
    {
        return $"{this.h} h, {this.min} m, {this.sec} s.";
    }
    public long getHashCode()
    {
        return Convert.ToInt64($"{this.h}{this.min}{this.sec}");
    }
    static void info()
    {
        Console.WriteLine("info");
    }
    public int getSec()
    {
        return this.sec;
    }
    public void setSec(int value)
    {
        this.sec = value;
    }
    public void test_0(ref int value)
    {
        value += 21;
    }
    public void test_1(out int value)
    {
        value = 21;
    }
    public string getTimeFormat()
    {
        return this.timeFormat;
    }

    static Time()
    {
        defaultFormat = "24h";
    }
    // 3
    // private Time(int hours, int minuts, int seconds)
    // {
    //     if (hours > 23)
    //     {
    //         this.h = 0;
    //     }
    //     else
    //     {
    //         this.h = hours;
    //     }
    //     if (minuts > 59)
    //     {
    //         this.min = 0;
    //     }
    //     else
    //     {
    //         this.min = minuts;
    //     }
    //     if (seconds > 59)
    //     {
    //         this.sec = 0;
    //     }
    //     else
    //     {
    //         this.sec = seconds;
    //     }
    // }
    // public object copy()
    // {
    //     return new Time(this.h, this.min, this.sec);
    // }
    public Time(int hours, int minuts, int seconds)
    {
        numberOfObjects++;
        if (hours > 23)
        {
            this.h = 0;
        }
        else
        {
            this.h = hours;
        }
        if (minuts > 59)
        {
            this.min = 0;
        }
        else
        {
            this.min = minuts;
        }
        if (seconds > 59)
        {
            this.sec = 0;
        }
        else
        {
            this.sec = seconds;
        }
    }
    public Time(int minuts, int seconds)
    {
        numberOfObjects++;
        if (seconds > 59)
        {
            this.sec = 0;
        }
        else
        {
            this.sec = seconds;
        }
        int hours = minuts / 60;
        minuts %= 60;
        if (hours > 23)
        {
            this.h = 0;
        }
        else
        {
            this.h = hours;
        }
        if (minuts > 59)
        {
            this.min = 0;
        }
        else
        {
            this.min = minuts;
        }
    }
    public Time(int seconds)
    {
        numberOfObjects++;
        int minuts = seconds / 60;
        seconds %= 60;
        int hours = minuts / 60;
        minuts %= 60;
        if (hours > 23)
        {
            this.h = 0;
        }
        else
        {
            this.h = hours;
        }
        if (minuts > 59)
        {
            this.min = 0;
        }
        else
        {
            this.min = minuts;
        }
        if (seconds > 59)
        {
            this.sec = 0;
        }
        else
        {
            this.sec = seconds;
        }
    }

}