using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    [Serializable]
    public class InsufficientFundsException:Exception
    {
        public InsufficientFundsException(decimal amountRequested, decimal accountBalance)
        {
            AmountRequested = amountRequested;
            AccountBalance = accountBalance;
        }

        public decimal AmountRequested { get; private set; }

        public decimal AccountBalance { get; private set; }
    }
}
