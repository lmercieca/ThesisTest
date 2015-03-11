using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    public class Fund : TestProject.IFund
    {
        public string ISIN { get; set; }
        public string FundName { get; set; }
        public Currency Currency { get; set; }
        public decimal NAV { get; set; }

        public Fund(string ISIN, string FundName, Currency currency, decimal NAV)
        {
            this.ISIN = ISIN;
            this.FundName = FundName;
            this.Currency = currency;
            this.NAV = NAV;
        }
    }
}
