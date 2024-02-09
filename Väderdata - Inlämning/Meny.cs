using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text.RegularExpressions;
using Väderdata___Inlämning;

namespace Meny
{
    public class Meny
    {

        public void ShowMenu()
        {
            Regex dateRegex = new Regex(@"(?<year>\d{4})-(?<month>0[1-9]|1[0-2])-(?<day>0[1-9]|[12]\d|3[01])");
            int indicator;
            bool exit = false;
            string outputPath = "../../../TextFile.txt/";
            while (!exit)
            {
                Console.WriteLine("Välkommen till väderapplikationen!");
                Console.WriteLine("Välj en funktion:");
                Console.WriteLine("1. Medeltemperatur och medelluftfuktighet per dag, för valt datum");
                Console.WriteLine("2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                Console.WriteLine("3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                Console.WriteLine("4. Sortering av minst till störst risk av mögel");
                Console.WriteLine("5. Datum för meteorologisk Höst");
                Console.WriteLine("6. Datum för meteorologisk vinter (OBS Mild vinter!)");
                Console.WriteLine("7. Avsluta programmet");



                string choice = Console.ReadLine();
                Console.WriteLine("Önskar du se Ute eller Inne temperatur?");
                string vald = Console.ReadLine().ToUpper();
                if (vald == "UTE")
                {
                    indicator = 1;
                }
                else
                {
                    indicator = 2;
                }
                switch (choice)
                {
                  

                    case "1":
                        Console.WriteLine("Ange ett datum: (YYYY-MM-DD)");
                        string input = Console.ReadLine();
                        Match match = dateRegex.Match(input);
                        if (match.Success)
                        {
                            DateTime date = DateTime.ParseExact(input, "yyyy-MM-dd", null);
                            Tuple<float, int> values = TemperatureData.AverageValuesOfDay(date, TemperatureData.OutputData(TemperatureData.GetTempData(), indicator));
                            Console.WriteLine("Medeltemp är: " + values.Item1);
                            Console.WriteLine("Medelluftfuktigheten är: " + values.Item2);
                        }
                        else
                        {
                            Console.WriteLine("Fel inmatning.");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Tryck på valfri knapp");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        TemperatureData.PrintColdestDay(TemperatureData.OutputData(TemperatureData.GetTempData(), indicator));
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        TemperatureData.PrintHumidityDay(TemperatureData.OutputData(TemperatureData.GetTempData(), indicator));
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    case "6":

                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Var vänlig välj ett giltigt alternativ.");
                        break;
                }


            }
        }

    }
}
