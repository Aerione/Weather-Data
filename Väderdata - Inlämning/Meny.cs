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

            string outputPath = "../../../Files/TextFile.txt";
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
                Console.WriteLine("7. Skriv resultat till fil");
                Console.WriteLine("8. Avsluta programmet");


                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.WriteLine("Önskar du se Ute eller Inne temperatur?");
                string choice = Console.ReadLine().ToUpper();
                Console.Clear();
                if (choice == "UTE")
                {
                    Console.WriteLine("Visar mätningar för utemiljö: ");
                    indicator = 1;
                }
                else
                {
                    Console.WriteLine("Visar mätningar för innemiljö: ");
                    indicator = 2;
                }
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Ange ett datum: (YYYY-MM-DD)");
                        string input = Console.ReadLine();
                        Match match = dateRegex.Match(input);
                        if (match.Success)
                        {
                            DateTime date = DateTime.ParseExact(input, "yyyy-MM-dd", null);
                            Tuple<float, int> values = TemperatureData.AverageValuesOfDay(date, TemperatureData.OutputData(indicator));
                            Console.WriteLine("Medeltemperaturen för dagen är: " + Math.Round(values.Item1, 2));
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
                    case ConsoleKey.D2:
                        var list = TemperatureData.SortMeanValuesByDay(TemperatureData.OutputData(indicator)).OrderByDescending(x => x.MeanTemperature).ToList();
                        foreach (var item in list)
                        {
                            Console.WriteLine("Datum: " + item.DateStamp.Date.ToString("yyyy/MM/dd") + " | Medeltemperatur: " + Math.Round(item.MeanTemperature, 2) + " C" + " | Medelfuktighet " + item.MeanHumidity + "%");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                        var list2 = TemperatureData.SortMeanValuesByDay(TemperatureData.OutputData(indicator)).OrderBy (x => x.MeanHumidity).ToList();
                        foreach (var item in list2)
                        {
                            Console.WriteLine("Datum: " + item.DateStamp.Date.ToString("yyyy/MM/dd") + " | Medeltemperatur: " + Math.Round(item.MeanTemperature, 2) + " C" + " | Medelfuktighet " + item.MeanHumidity + "%");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                        //Placeholder, behöver formatera mögelindexen
                        break;
                    case ConsoleKey.D5:
                        if (indicator == 2)
                        {
                            Console.WriteLine("Denna funktion är tyvärr inte tillgänglig för det valda alternativet, tryck valfri knapp för att gå vidare");
                            Console.ReadKey();
                            break;
                        }
                        int consecutiveAutumnDays = 0;
                        var list3 = TemperatureData.SortMeanValuesByDay(TemperatureData.OutputData(indicator));
                        foreach (var entry in list3)
                        {
                            if (entry.MeanTemperature < 10)
                            {
                                consecutiveAutumnDays++;
                            }
                            else
                            {
                                consecutiveAutumnDays = 0;
                            }

                            if (consecutiveAutumnDays == 5)
                            {
                                Console.WriteLine($"{entry.DateStamp.AddDays(-4):yyyy-MM-dd} var första meteorogiska höstdagen med medeltemperaturen {Math.Round(entry.MeanTemperature, 2)} C.");
                                Console.ReadKey();
                                break;
                            }
                        }
                        break;
                    case ConsoleKey.D6:
                        if (indicator == 2)
                        {
                            Console.WriteLine("Denna funktion är tyvärr inte tillgänglig för det valda alternativet, tryck valfri knapp för att gå vidare");
                            Console.ReadKey();
                            break;
                        }
                        int consecutiveWinterDays = 0;
                        var list4 = TemperatureData.SortMeanValuesByDay(TemperatureData.OutputData(indicator));
                        foreach (var entry in list4)
                        {
                            if (entry.MeanTemperature < 0)
                            {
                                consecutiveWinterDays++;
                            }
                            else
                            {
                                consecutiveWinterDays = 0;
                            }

                            if (consecutiveWinterDays == 5)
                            {
                                Console.WriteLine($"{entry.DateStamp.AddDays(-4):yyyy-MM-dd} var första meteorogiska vinterdagen med medeltemperaturen {Math.Round(entry.MeanTemperature, 2)} C.");
                                Console.ReadKey();
                                break;
                            }
                        }
                        break;
                    case ConsoleKey.D7:
                        ReadWriteFile.WriteAll(outputPath);
                        Console.WriteLine("Fil uppdaterad");
                        break;
                    case ConsoleKey.D8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Vänligen välj ett giltigt alternativ.");
                        break;
                }


            }
        }

    }
}
