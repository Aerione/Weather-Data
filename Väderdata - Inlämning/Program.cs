using System;
using System.Text.RegularExpressions;
using Väderdata___Inlämning;

class Program
{
    static void Main()
    {
        List<string> tempData = new List<string>();
        List<Data> uDataList = new List<Data>();

        string filePath = "../../../Files/tempData.txt";

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                tempData.Add(line); 
            }
        }


        uDataList = TemperatureData.OutputData(tempData, 1);

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
