using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestProject;

namespace FundPlatform.Console
{
    
    class Program
    {
        static Client clt = null;

        
        static void Main(string[] args)
        {
            clt = new Client() { Name = "TestName", Surname = "TestSurname" };
            
            ExchangeManager em = new ExchangeManager();
            System.Console.WriteLine(em.ConvertValue(Currency.USD,Currency.GBP,1));

            AccountManager acctManager = new AccountManager();

            acctManager.CashTransfer(clt, Currency.EUR, 100);
            PrintAccounts("Cash transfer 100 EUR");

            acctManager.CashTransfer(clt, Currency.GBP, 100);
            PrintAccounts("Cash transfer 100 GBP");

            acctManager.CashDisbursement(clt, Currency.EUR, 10);
            PrintAccounts("Cash disbursement 10 EUR");
                        
            FundManager fundManager = new FundManager();
            fundManager.BuyFund(clt, FundManager.Funds.ElementAt(0).Key, 50);
            PrintAccounts("Bought " + FundManager.Funds.ElementAt(0).Key + " 50 with currency " + ((Fund)FundManager.Funds.ElementAt(0).Value).Currency );

            fundManager.BuyFund(clt, FundManager.Funds.ElementAt(3).Key, 50);
            PrintAccounts("Bought " + FundManager.Funds.ElementAt(3).Key + " 50 with currency " + ((Fund)FundManager.Funds.ElementAt(3).Value).Currency);

            fundManager.BuyFund(clt, FundManager.Funds.ElementAt(5).Key, 50);
            PrintAccounts("Bought " + FundManager.Funds.ElementAt(5).Key + " 50 with currency " + ((Fund)FundManager.Funds.ElementAt(5).Value).Currency);

            fundManager.SellFund(clt, FundManager.Funds.ElementAt(0).Key, 30);
            PrintAccounts("Sold " + FundManager.Funds.ElementAt(0).Key + " 50 with currency " + ((Fund)FundManager.Funds.ElementAt(0).Value).Currency);
            
            PrintTransactions();
            PrintFunds();

            System.Console.ReadLine();
        }

        private static void PrintAccounts(string message)
        {            
            if ( !String.IsNullOrEmpty(message))
            {                
                System.Console.WriteLine(message);
                System.Console.WriteLine("------");
            }

            foreach (Account acc in clt.GetAccounts())
            {
                System.Console.WriteLine("Currency: " + acc.Currency + "\tBalance: " + acc.Balance );
            }

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine();
        }

        private static void PrintTransactions()
        {            
            foreach (Transaction trs in clt.GetTransactions())
            {
                if ( trs.Fund == null)
                    System.Console.WriteLine("Transaction Type: " + trs.TransactionType + "\tCurrency: " + trs.Currency + "\tOld Balance:\t " + trs.FromAmount + "\tNew Balance: \t" + trs.NewAmount);
                else
                    System.Console.WriteLine("Transaction Type: " + trs.TransactionType + "\tCurrency: " + trs.Currency + "\tOld Balance:\t " + trs.FromAmount + "\tNew Balance: \t" + trs.NewAmount + "\tFund:\t" + trs.Fund.FundName);
            }
            
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine();
        }

        private static void PrintFunds()
        {
            foreach (KeyValuePair<Fund,decimal> fnd in clt.GetFunds())
            {
                System.Console.WriteLine("ISIN: " + fnd.Key.FundName + "\tFund Name " + fnd.Key.ISIN + "\tCurrency: " + fnd.Key.Currency  + "\tNAV: \t" + fnd.Key.NAV + "\tAmount: \t" + fnd.Value);
            }

            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine();
        }
    }
}
