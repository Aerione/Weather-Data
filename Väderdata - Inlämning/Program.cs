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

        float averageTemperature = uDataList
            .Where(data => data.DateTime.Month == 6 && data.DateTime.Day == 8 && data.DateTime.Year == 2016)
            .Average(data => data.Temperature);

        Console.WriteLine("Average Value = " + averageTemperature);
        Console.ReadKey();

        //foreach (Data data in uDataList)
        //{
        //    Console.WriteLine(data.DateTime + " " + data.Temperature + " " + data.Humidity);
        //}

    }
}
