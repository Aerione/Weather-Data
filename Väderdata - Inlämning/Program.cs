using System;
using System.Text.RegularExpressions;
using Väderdata___Inlämning;

class Program
{
    static void Main()
    {


        Temperaturedata temperatureDataHandler = new Temperaturedata();

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
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
                    DataInfo.OutdoorData("../../../Files/TempData.txt");


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


        //    List<string> tempData = new List<string>();
        //    List<Data> uDataList = new List<Data>();

        //    string filePath = "../../../Files/tempData.txt";

        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            tempData.Add(line);
        //        }
        //    }


        //    uDataList = Temperaturedata.OutputData(tempData, 1);

        //    float averageTemperature = uDataList
        //        .Where(data => data.DateTime.Month == 6 && data.DateTime.Day == 8 && data.DateTime.Year == 2016)
        //        .Average(data => data.Temperature);

        //    Console.WriteLine("Average Value = " + averageTemperature);
        //    Console.ReadKey();

        //foreach (Data data in uDataList)
        //{
        //    Console.WriteLine(data.DateTime + " " + data.Temperature + " " + data.Humidity);
        //}

    }
}
