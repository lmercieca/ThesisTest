using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{

    public class ExchangeManager : TestProject.IExchangeManager 
    {
        private static Dictionary<Currency, decimal> rates = null;
        public static Dictionary<Currency, decimal> Rates
        {
            get
            {
                if (rates == null)
                {
                    rates = new Dictionary<Currency, decimal>();

                    rates.Add(Currency.GBP, 1.1676786550m);
                    rates.Add(Currency.USD, 0.7663422480m);
                    rates.Add(Currency.EUR, 1);
                }

                return rates;
            }
        }

        public decimal ConvertValue(Currency from, Currency to, decimal amount)
        {

            return ((amount * Rates[from]) / Rates[to]);
        }

    }
}
