using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Väderdata___Inlämning
{
    internal class TemperatureData
    {
        public static List<Data> OutputData(List<string> tempData, int indicator)
        {
            string location = "";
            List<Data> dataList = new List<Data>();
            Regex tempRegex = new Regex(@"(?<year>\d{4})-(?<month>0[1-9]|1[0-2])-(?<day>0[1-9]|[12]\d|3[01]) (?<hours>([01]\d|2[0-3])):(?<minutes>[0-5]\d):(?<seconds>[0-5]\d),(?<indicator>\w+),(?<temp>-?([0-9]\d*(\.\d+)?)),(?<humidity>(?:100|\d{1,2}))");

            if (indicator == 1)
            {
                location = "Ute";
            }
            if (indicator == 2)
            {
                location = "Inne";
            }

            foreach (string data in tempData)
            {
                Match match = tempRegex.Match(data);

                if (match.Success)
                {
                    if (!((int.Parse(match.Groups["year"].Value) == 2016) && (int.Parse(match.Groups["month"].Value) == 5)) && !((int.Parse(match.Groups["year"].Value) == 2017) && (int.Parse(match.Groups["month"].Value) == 1)))
                    {
                        if (match.Groups["indicator"].Value == location)
                        {
                            int year = int.Parse(match.Groups["year"].Value);
                            int month = int.Parse(match.Groups["month"].Value);
                            int day = int.Parse(match.Groups["day"].Value);
                            int hours = int.Parse(match.Groups["hours"].Value);
                            int minutes = int.Parse(match.Groups["minutes"].Value);
                            int seconds = int.Parse(match.Groups["seconds"].Value);
                            float temp = float.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture);
                            int humidity = int.Parse(match.Groups["humidity"].Value);

                            DateTime dateTime = new DateTime(year, month, day, hours, minutes, seconds);

                            dataList.Add(new Data(dateTime, temp, humidity));

                        }
                    }
                }

            }
            return dataList;
        }

        public static List<string> GetTempData() 
        {
            List<string> dataList = new List<string>();

            string filePath = "../../../Files/tempData.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    dataList.Add(line);
                }
            }

            return dataList;
        }

        public static Tuple<float, int> AverageValuesOfDay(DateTime date, List<Data> tempList)
        {
            var groupedEntries = tempList.GroupBy(e => e.DateTime.Date);
            Tuple<float, int> valueTuple = new Tuple<float, int>(0,0);
            var meanValues = groupedEntries.Select(group =>
            {
                float meanTemperature = group.Select(e => e.Temperature).Sum() / group.Count();
                int meanHumidity = group.Select(e => e.Humidity).Sum() / group.Count();
                return new
                {
                    DateStamp = group.Key,
                    MeanTemperature = meanTemperature,
                    MeanHumidity = meanHumidity,
                };
            });

            foreach (var item in meanValues)
            {
                if (item.DateStamp.Year == date.Year && item.DateStamp.Month == date.Month && item.DateStamp.Date == date.Date)
                {
                    Tuple<float, int> returnValue = new Tuple<float, int>(item.MeanTemperature, item.MeanHumidity);
                    return returnValue;
                }
            }
            Console.WriteLine("Varning, inget värde finns för angivet datum.");
            return valueTuple;
        }

        public static void PrintColdestDay(List<Data> tempList)
        {
            var groupedEntries = tempList.GroupBy(e => e.DateTime.Date);
            var meanValues = groupedEntries.Select(group =>
            {
                float meanTemperature = group.Select(e => e.Temperature).Sum() / group.Count();
                int meanHumidity = group.Select(e => e.Humidity).Sum() / group.Count();

                return new
                {
                    DateStamp = group.Key,
                    MeanTemperature = meanTemperature,
                    MeanHumidity = meanHumidity,
                };
            });

            var sortedMeans = meanValues.OrderBy(x => x.MeanTemperature);

            foreach (var meanValue in sortedMeans)
            {
                Console.WriteLine($"Date: {meanValue.DateStamp:yyyy-MM-dd}");
                Console.WriteLine($"Mean Temperature: {meanValue.MeanTemperature}");
                Console.WriteLine($"Mean Humidity: {meanValue.MeanHumidity}");
                Console.WriteLine();
            }
        }

        public static void PrintHumidityDay(List<Data> tempList)
        {
            var groupedEntries = tempList.GroupBy(e => e.DateTime.Date);
            var meanValues = groupedEntries.Select(group =>
            {
                float meanTemperature = group.Select(e => e.Temperature).Sum() / group.Count();
                int meanHumidity = group.Select(e => e.Humidity).Sum() / group.Count();

                return new
                {
                    DateStamp = group.Key,
                    MeanTemperature = meanTemperature,
                    MeanHumidity = meanHumidity,
                };
            });

            var sortedMeans = meanValues.OrderBy(x => x.MeanHumidity);

            // Print sorted mean values
            foreach (var meanValue in sortedMeans)
            {
                Console.WriteLine($"Date: {meanValue.DateStamp:yyyy-MM-dd}");
                Console.WriteLine($"Mean Temperature: {meanValue.MeanTemperature}");
                Console.WriteLine($"Mean Humidity: {meanValue.MeanHumidity}");
                Console.WriteLine();
            }
        }

        public static void PrintAverage()
        {
           List<Data> uDataList = new List<Data>();


            uDataList = TemperatureData.OutputData(GetTempData(), 1);

            //float averageTemperature = uDataList
            //    .Where(data => data.DateTime.Month == 6 && data.DateTime.Day == 8 && data.DateTime.Year == 2016)
            //    .Average(data => data.Temperature);

            //Console.WriteLine("Average Value = " + averageTemperature);
            //Console.ReadKey();

            var groupedEntries = uDataList.GroupBy(e => e.DateTime.Date);

            // Calculate mean values for each property for each date stamp
            var meanValues = groupedEntries.Select(group =>
            {
                float meanTemperature = group.Select(e => e.Temperature).Sum() / group.Count();
                int meanHumidity = group.Select(e => e.Humidity).Sum() / group.Count();
                //Mold index not totally correct
                double moldIndex = ((meanHumidity - 78) * ((meanTemperature / 15) / 0.22));

                return new
                {
                    DateStamp = group.Key,
                    MeanTemperature = meanTemperature,
                    MeanHumidity = meanHumidity,
                    MoldIndex = moldIndex
                };
            });

            foreach (var item in meanValues)
            {
                Console.WriteLine("Date: " + item.DateStamp.Year + ":" + item.DateStamp.Month + ":" + item.DateStamp.Day);
                Console.WriteLine("Mean Temperature: " + item.MeanTemperature);
                Console.WriteLine("Mean Humidity " + item.MeanHumidity);
                Console.WriteLine("Mold Index " + item.MoldIndex);
                Console.WriteLine();
            }

            //foreach (Data data in uDataList)
            //{
            //    Console.WriteLine(data.DateTime + " " + data.Temperature + " " + data.Humidity);
            //}
        }
    }
}
