using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text.RegularExpressions;
using Väderdata___Inlämning;

namespace Meny
{
    internal class Meny
    {
        public void Menu(List<Data> tempData)
        {
            string filePath = "../../../Files/TempData.txt";


            bool exit = false;
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

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Ange ett datum (ÅÅÅÅ-MM-DD): ");
                        string inputDate = Console.ReadLine();

                        if (DateTime.TryParse(inputDate, out DateTime chosenDate))
                        {
                            var measurementsForDate = tempData
                                .Where(m => m.DateTime.Date == chosenDate.Date)
                                .ToList();

                            if (measurementsForDate.Any())
                            {
                                double averageTemperature = measurementsForDate.Average(m => m.Temperature);
                                double averageHumidity = measurementsForDate.Average(m => m.Humidity);
                                Console.WriteLine($"Medeltemperatur för {chosenDate.ToShortDateString()}: {averageTemperature} °C");
                                Console.WriteLine($"Medelluftfuktighet för {chosenDate.ToShortDateString()}: {averageHumidity}%");
                            }
                            else
                            {
                                Console.WriteLine($"Inga mätningar finns för {chosenDate.ToShortDateString()}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt datumformat. Var vänlig ange datumet i formatet ÅÅÅÅ-MM-DD.");
                        }
                        break;
                    case "2":

                        break;
                    case "3":

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


        public void ShowAverageTemperatureAndHumidityForDate(DateTime chosenDate)
        {
        
        }



        public static void SortByTemperature(List<Data> measurements)
        {

        }

        public static void SortByHumidity(List<Data> measurements)
        {

        }

        public static void SortByMoldRisk(List<Data> measurements)
        {

        }

        public static void ShowAutumnDate(List<Data> measurements)
        {

        }

        public static void ShowMildWinterDate(List<Data> measurements)
        {

        }

        private static void DisplayMeasurements(List<Data> measurements)
        {

        }

    }
}
