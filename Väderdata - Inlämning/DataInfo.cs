using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Väderdata___Inlämning
{
    internal class DataInfo
    {
        public static void OutdoorData(string filepath)
        {
            Temperaturedata temperatureData = new Temperaturedata();
            temperatureData.LoadMeasurementsFromFile(filepath);
            while (true)
            {
                string filePath = "../../../Files/TempData.txt";
                Console.Clear();
                Console.WriteLine("Ange ett datum (YYYY-MM-DD):");
                string date = Console.ReadLine();

                if (DateTime.TryParse(date, out DateTime chosenDate))
                {
                    Console.WriteLine($"Valt datum: {chosenDate}");

                    var allMeasurements = temperatureData.data;

                    Console.WriteLine("Tillgängliga mätningar:");
                    foreach (var measurement in allMeasurements)
                    {
                        Console.WriteLine($"{measurement.DateTime.ToShortDateString()}");
                    }

                    var measurementsForChosenDate = allMeasurements
                        .Where(m => m.DateTime.Date == chosenDate.Date)
                        .ToList();

                    if (measurementsForChosenDate.Any())
                    {
                        double averageTemperature = measurementsForChosenDate.Average(m => m.Temperature);
                        double averageHumidity = measurementsForChosenDate.Average(m => m.Humidity);
                        Console.WriteLine($"Genomsnittlig temperatur {chosenDate.ToShortDateString()}: {averageTemperature} °C");
                        Console.WriteLine($"Genomsnittlig luftfuktighet {chosenDate.ToShortDateString()}: {averageHumidity}%");
                    }
                    else
                    {
                        Console.WriteLine($"Inga mätningar hittades {chosenDate.ToShortDateString()}.");
                    }
                }
                else
                {
                    Console.WriteLine("Fel inmatning av datum.");
                }

                Console.WriteLine();
                Console.WriteLine("Tryck på valfri knapp för att prova igen");
                Console.WriteLine("Tryck på m för att återgå till huvudmenyn");
                ConsoleKeyInfo mainmenu = Console.ReadKey();
                if (mainmenu.KeyChar == 'm')
                {
                    break;
                }
            }


            //while (true)
            //{
            //    Console.Clear();
            //    Console.WriteLine("Ange ett datum: ");
            //    string date = Console.ReadLine();

            //    if (DateTime.TryParse(date, out DateTime chosenDate))
            //    {
            //        var temperatureDataInstance = new Temperaturedata();
            //        temperatureDataInstance.ShowAverageTemperatureAndHumidityForDate(chosenDate);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Fel format på datumet.");
            //    }

            //    Console.WriteLine();
            //    Console.WriteLine("Tryck på valfri knapp för att prova igen");
            //    Console.WriteLine("Tryck på m för att gå tillbaka till huvudmenyn");
            //    ConsoleKeyInfo mainmenu = Console.ReadKey();
            //    if (mainmenu.KeyChar == 'm')
            //    {
            //        break;
            //    }
            //}
            //Console.Clear();
        }



        public static void OutdoorAveragePerDay()
        {

        }

        public static void IndoorAveragePerDay()
        {

        }
        public static void IndoorData()

        {


        }

        public static void Autumn()
        {

        }

        public static void Winter()
        {

        }
    }
}

