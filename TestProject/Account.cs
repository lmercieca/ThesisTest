using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    public enum Currency {EUR,USD,GBP};

    public class Account : TestProject.IAccount
    {
        public Currency Currency { get; private set; }
        public decimal Balance { get; private set; }

        public Account(Currency currency, decimal amount)
        {
            this.Currency = currency;
            this.Balance = amount;
        }

        public void AddBalance(decimal amount)
        {
            Balance += amount;
        }

        public void ReduceBalance(decimal amount)
        {
            Balance -= amount;
        }        
    }
}
