using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    public class FundManager : TestProject.IFundManager
    {
        private static  Dictionary<string, Fund> funds = null;
        public static Dictionary<string, Fund> Funds
        {
            get
            {
                if (funds == null)
                {
                    funds = new Dictionary<string, Fund>();

                    funds.Add("XS0509465331", new Fund("XS0509465331", "RBS 3 Index Autocallable Note 3 EUR", Currency.EUR, 138.4800m));
                    funds.Add("XS0793515049", new Fund("XS0793515049", "Morgan Stanley 5Y Quarterly Inc USD", Currency.USD, 115.1600m));
                    funds.Add("XS0514499598", new Fund("XS0514499598", "RBS Multi-Index Autocall Nt 8 EUR", Currency.EUR, 91.4000m));
                    funds.Add("XS0687684547", new Fund("XS0687684547", "GS 5Y Auto Quanto Developed Mkt GBP", Currency.GBP, 112.7000m));
                    funds.Add("IE0008368742", new Fund("IE0008368742", "First State China Growth I USD Cap", Currency.USD, 105.7900m));
                    funds.Add("LU0278932362", new Fund("LU0278932362", "Aberdeen Gl Emg Mkt Sm Comp D2 Cap", Currency.GBP, 12.9966m));
                    funds.Add("XS0514495414", new Fund("XS0514495414", "Multi Idx Autoc Note 8 Emrg Mkt EUR", Currency.EUR, 90.5000m));
                    funds.Add("LU0266512127", new Fund("LU0266512127", "JPM Gl Natural Resources A USD Cap", Currency.USD, 10.4300m));
                    funds.Add("LU0055114457", new Fund("LU0055114457", "Fidelity Indonesia Fund A", Currency.USD, 34.2700m));
                    funds.Add("LU0362742479", new Fund("LU0362742479", "Jupiter Merlin Intl Bal EUR Cap", Currency.EUR, 17.0300m));
                }

                return funds;
            }
        }

        public Fund GetFundByISIN(string ISIN)
        {
            return funds[ISIN];
        }

        public void BuyFund(Client client, string ISIN, decimal amount)
        {
            client.BuyFund(ISIN, amount);
        }

        public void SellFund(Client client, string ISIN, decimal amount)
        {
            client.SellFund(ISIN, amount);
        }
    }
}
