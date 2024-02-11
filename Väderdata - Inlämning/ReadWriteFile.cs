using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Väderdata___Inlämning
{
    internal class ReadWriteFile
    {

        public static string path = "../../../TextFile/";
        public static void ReadAll(string fileName)
        {
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                string line = reader.ReadLine();
                int rowCount = 0;
                while (line != null)
                {
                    Console.WriteLine(rowCount + " " + line);
                    rowCount++;
                    line = reader.ReadLine();
                }
            }
        }

        public static void WriteAll(string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine("Medeltemperatur sorterad efter månad (Ute):");

                List<(DateTime, float, int)> monthlyMeanValues = TemperatureData.SortMeanValuesByMonth(TemperatureData.OutputData(1));

                foreach (var meanValues in monthlyMeanValues)
                {
                    streamWriter.WriteLine("Månad: " + meanValues.Item1.Month);   
                    streamWriter.WriteLine("Temperatur: " + meanValues.Item2);
                    streamWriter.WriteLine("Fuktighet " + meanValues.Item3);
                }

                streamWriter.WriteLine("Medelfuktighet sorterad efter månad (Inne):");

                List<(DateTime, float, int)> monthlyMeanValues2 = TemperatureData.SortMeanValuesByMonth(TemperatureData.OutputData(2));

                foreach (var meanValues in monthlyMeanValues2)
                {
                    streamWriter.WriteLine("Månad: " + meanValues.Item1.Month);
                    streamWriter.WriteLine("Temperatur: " + meanValues.Item2);
                    streamWriter.WriteLine("Fuktighet " + meanValues.Item3);
                }

                int consecutiveAutumnDays = 0;
                List<(DateTime, float, int)> list = TemperatureData.SortMeanValuesByDay(TemperatureData.OutputData(1));
                foreach (var entry in list)
                {
                    if (entry.Item2 < 10)
                    {
                        consecutiveAutumnDays++;
                    }
                    else
                    {
                        consecutiveAutumnDays = 0;
                    }

                    if (consecutiveAutumnDays == 5)
                    {
                        streamWriter.WriteLine($"{entry.Item1.AddDays(-4):yyyy-MM-dd} var första meteorogiska höstdagen med medeltemperaturen {Math.Round(entry.Item2, 2)} C.");
                        break;
                    }
                }

                streamWriter.WriteLine("Datum för meteorogiska årstider:");

                int consecutiveWinterDays = 0;
                foreach (var entry in list)
                {
                    if (entry.Item2 < 0)
                    {
                        consecutiveWinterDays++;
                    }
                    else
                    {
                        consecutiveWinterDays = 0;
                    }

                    if (consecutiveWinterDays == 5)
                    {
                        streamWriter.WriteLine($"{entry.Item1.AddDays(-4):yyyy-MM-dd} var första meteorogiska vinterdagen med medeltemperaturen {Math.Round(entry.Item2, 2)} C.");
                        break;
                    }
                }
                streamWriter.WriteLine("Formeln för mögelindexen: " + "((luftfuktighet -78) * (Temp/15))/0,22");
            }
        }


    }

}
