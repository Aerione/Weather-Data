using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Väderdata___Inlämning
{
    public class Temperaturedata
    {
        public List<Data> data;

        public Temperaturedata()
        {
            data = new List<Data>();
        }

        public void LoadMeasurementsFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Regex tempRegex = new Regex(@"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2}) (?<hours>\d{2}):(?<minutes>\d{2}):(?<seconds>\d{2}),(?<indicator>\w+),(?<temp>[\d.]+),(?<humidity>\d+)");

                    Match match = tempRegex.Match(line);

                    if (match.Success)
                    {
                        int year = int.Parse(match.Groups["year"].Value);
                        int month = int.Parse(match.Groups["month"].Value);
                        int day = int.Parse(match.Groups["day"].Value);
                        int hours = int.Parse(match.Groups["hours"].Value);
                        int minutes = int.Parse(match.Groups["minutes"].Value);
                        int seconds = int.Parse(match.Groups["seconds"].Value);
                        string indicator = match.Groups["indicator"].Value;
                        float temp;
                        int humidity;

                        if (float.TryParse(match.Groups["temp"].Value, out temp) && int.TryParse(match.Groups["humidity"].Value, out humidity))
                        {
                            DateTime dateTime = new DateTime(year, month, day, hours, minutes, seconds);
                            data.Add(new Data
                            {
                                DateTime = dateTime,
                                Temperature = temp,
                                Humidity = humidity
                            });
                        }

                    }

                }
            }
        }
    }

    //public class TemperatureData
    //{

    //    public static List<Data> OutputData(List<string> tempData, int indicator)
    //    {
    //        string location = "";
    //        List<Data> dataList = new List<Data>();
    //        Regex tempRegex = new Regex(@"(?<year>\d{4})-(?<month>0[1-9]|1[0-2])-(?<day>0[1-9]|[12]\d|3[01]) (?<hours>([01]\d|2[0-3])):(?<minutes>[0-5]\d):(?<seconds>[0-5]\d),(?<indicator>\w+),(?<temp>-?([0-9]\d*(\.\d+)?)),(?<humidity>(?:100|\d{1,2}))");

    //        if (indicator == 1)
    //        {
    //            location = "Ute";
    //        }
    //        if (indicator == 2)
    //        {
    //            location = "Inne";
    //        }

    //        foreach (string data in tempData)
    //        {
    //            Match match = tempRegex.Match(data);

    //            if (match.Success)
    //            {
    //                if (!((int.Parse(match.Groups["year"].Value) == 2016) && (int.Parse(match.Groups["month"].Value) == 5)) && !((int.Parse(match.Groups["year"].Value) == 2017) && (int.Parse(match.Groups["month"].Value) == 1)))
    //                {
    //                    if (match.Groups["indicator"].Value == location)
    //                    {
    //                        int year = int.Parse(match.Groups["year"].Value);
    //                        int month = int.Parse(match.Groups["month"].Value);
    //                        int day = int.Parse(match.Groups["day"].Value);
    //                        int hours = int.Parse(match.Groups["hours"].Value);
    //                        int minutes = int.Parse(match.Groups["minutes"].Value);
    //                        int seconds = int.Parse(match.Groups["seconds"].Value);
    //                        float temp = float.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture);
    //                        int humidity = int.Parse(match.Groups["humidity"].Value);

    //                        DateTime dateTime = new DateTime(year, month, day, hours, minutes, seconds);


    //                        dataList.Add(new Data
    //                        {
    //                            DateTime = dateTime,
    //                            Temperature = temp,
    //                            Humidity = humidity
    //                        });
    //                    }
    //                }
    //            }

    //        }
    //        return dataList;
    //    }
    //}
}
