using System;
using System.Text.RegularExpressions;
using Väderdata___Inlämning;
using Meny;

class Program
{
    static void Main()
    {
        string outputPath = "../../../Files/TextFile.txt";
        ReadWriteFile.WriteAll(outputPath);
        //Meny.Meny meny = new Meny.Meny();
        //meny.ShowMenu();
        //TemperatureData.PrintAverage();
    }
}
