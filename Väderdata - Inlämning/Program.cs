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

        foreach (Data data in uDataList)
        {
            Console.WriteLine(data.DateTime + " " + data.Temperature + " " + data.Humidity);
        }

    }
}
