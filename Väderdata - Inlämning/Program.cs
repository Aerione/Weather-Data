using System;
using System.Text.RegularExpressions;
using Väderdata___Inlämning;
using Meny;

class Program
{
    static void Main()
    {
        Meny.Meny meny = new Meny.Meny();
        meny.ShowMenu();
        TemperatureData.PrintAverage();
    }
}
