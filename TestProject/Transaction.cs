using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TestProject
{
    public enum TransType { Buy, Sell, CT, CD }

    public class Transaction
    {
        public TransType TransactionType { get; set; }
        public decimal NewAmount { get; set; }
        public decimal FromAmount { get; set; }
        public Fund Fund { get; set; }
        public Currency Currency { get; set; }

        public Transaction(TransType transactionType, decimal newAmount, decimal fromAmount, Fund fund, Currency currency)
        {
            this.TransactionType = transactionType;
            this.NewAmount = newAmount;
            this.FromAmount = fromAmount;
            this.Fund = fund;
            this.Currency = currency;
        }
    }
}
