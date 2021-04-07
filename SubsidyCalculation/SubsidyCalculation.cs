using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsidyCalculation
{
    public class SubsidyCalculation : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            throw new NotImplementedException();
        }
    }
}
