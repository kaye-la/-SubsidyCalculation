using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsidyCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Volume volume = new Volume()
            {
                HouseId = 0,
                ServiceId = 0,
                Month = new DateTime(2021, 5, 10),
                Value = 10
            };
            Tariff tariff = new Tariff()
            {
                ServiceId = 0,
                HouseId = 0,
                PeriodBegin = new DateTime(2021, 1, 1),
                PeriodEnd = new DateTime(2021, 12, 1),
                Value = 10
            };

            SubsidyCalculation subsidyCalculation = new SubsidyCalculation();
            subsidyCalculation.OnNotify += OnNotify;
            subsidyCalculation.OnException += OnException;

            Charge charge = subsidyCalculation.CalculateSubsidy(volume, tariff);
            Console.WriteLine($"Subsidy calculation = {charge.Value}\n");
            Console.ReadKey();
        }

        private static void OnNotify(object sender, string msg)
        {
            Console.WriteLine(msg);
        }

        private static void OnException(object sender, Tuple<string, Exception> msg)
        {
            Console.WriteLine(msg.Item2.Message);
        }
    }
}
