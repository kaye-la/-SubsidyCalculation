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
            string msg = null;
            Charge charge = null;
            try
            {
                OnNotify?.Invoke(this, $"Расчёт начат в {DateTime.Now}");
                
                if (volumes.ServiceId != tariff.ServiceId)
                    msg += "Не совпадают ID услуг объема и тарифа!\n";
                if (volumes.HouseId != tariff.HouseId)
                    msg += "Не совпадают ID домов объема и тарифа!\n";
                if (tariff.PeriodBegin > volumes.Month  || volumes.Month > tariff.PeriodEnd)
                    msg += "Месяц объема не входит в период тарифа!\n";
                if (tariff.Value <= 0)
                    msg += "Значение тарифа не должно быть <= 0!\n";
                if (volumes.Value < 0)
                    msg += "Значение объема не может быть < 0!\n";
                
                if (msg != null)
                {
                    OnException?.Invoke(this, new Tuple<string, Exception>(msg, new Exception(msg)));
                    return null;
                }
                charge = new Charge()
                {
                    HouseId = volumes.HouseId,
                    ServiceId = volumes.ServiceId,
                    Month = volumes.Month,
                    Value = volumes.Value * tariff.Value
                };
                OnNotify?.Invoke(this, $"Расчёт успешно завершён в {DateTime.Now}");
            }
            catch (Exception e)
            {
                OnException?.Invoke(this, new Tuple<string, Exception>(e.Message, e));
            }
            return charge;
        }
    }
}
