using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OctoPrint.API;
using OctoPrint.API.Models;

namespace OctiPrint.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OctoPrintConnection api = new OctoPrintConnection("http://octopi.local/", "009FDBC4E3764AC79177C5C43C34A36F");

            Console.WriteLine("Press enter to exit....");

            Task.Run(() =>
            {
                while (true)
                {
                    PrinterModel printer = api.GetPrinter();
                    if (printer == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Printer not connected");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        string test = $"Tool Temp: {printer.Temperature.Tool0.Actual:N}/{printer.Temperature.Tool0.Target:N} |  Bed Temp: {printer.Temperature.Bed.Actual:N}/{printer.Temperature.Bed.Target:N}";

                        Console.WriteLine(test);
                    }

                    Task.Delay(1000);
                }
            });

            Console.ReadKey();
        }
    }
}
