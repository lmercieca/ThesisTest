using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Moq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject;
using NUnit.Framework;

namespace FundPlatform.Tests
{
    //[TestClass]    
    public class TestClient
    {
        //[TestMethod]
        [TestCase]
        public void TestCashTransfer()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);
            Assert.AreEqual(1, clt.GetAccounts().Length);
            Assert.AreEqual(Currency.EUR, clt.GetAccounts()[0].Currency);
            Assert.AreEqual(50, clt.GetAccounts()[0].Balance);
        }

        //[TestMethod]
        [TestCase]
        public void TestCashDisbursement()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);
            clt.AddBalanceToAccount(Currency.EUR, -30);

            Assert.AreEqual(1, clt.GetAccounts().Length);
            Assert.AreEqual(Currency.EUR, clt.GetAccounts()[0].Currency);
            Assert.AreEqual(20, clt.GetAccounts()[0].Balance);
        }

        //[TestMethod]
        [TestCase]
        public void TestCashDisbursementAndCloseAccount()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);
            clt.AddBalanceToAccount(Currency.EUR, -50);

            Assert.AreEqual(0, clt.GetAccounts().Length);
        }

        /// <summary>
        /// Aim of the test is to ensure if there exists an account of the same currency already, it
        /// is not recreated twice.
        /// </summary>
        //[TestMethod]
        [TestCase]
        public void TestDoubleCashTransfer()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);
            clt.AddBalanceToAccount(Currency.EUR, 100);

            Assert.AreEqual(1, clt.GetAccounts().Length);
            Assert.AreEqual(Currency.EUR, clt.GetAccounts()[0].Currency);
            Assert.AreEqual(150, clt.GetAccounts()[0].Balance);
        }

        //[TestMethod]
        [TestCase]
        public void TestCashTransferTransaction()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            Assert.AreEqual(1, clt.GetTransactions().Length);
            Assert.AreEqual(50, clt.GetTransactions()[0].NewAmount);
            Assert.AreEqual(0, clt.GetTransactions()[0].FromAmount);
            Assert.AreEqual(TransType.CT, clt.GetTransactions()[0].TransactionType);
            Assert.AreEqual(Currency.EUR, clt.GetTransactions()[0].Currency);
        }

        //[TestMethod]
        [TestCase]
        public void TestBuy()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 50);
            Assert.AreEqual(1, clt.GetFunds().Length);
            Assert.AreEqual(Isin, ((Fund)clt.GetFunds().ElementAt(0).Key).ISIN);
            Assert.AreEqual(50, clt.GetFunds().ElementAt(0).Value);
        }

        //[TestMethod]
        [TestCase]
        public void TestBuyTransactions()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 50);
            Assert.AreEqual(2, clt.GetTransactions().Length, "Testing number of transactions is 2, one CT and one Buy.");
            Assert.AreEqual(Isin, clt.GetTransactions()[1].Fund.ISIN, "Testing that the added ISIN is correct.");
            Assert.AreEqual(0, clt.GetTransactions()[1].NewAmount, "Testing that the new amount in the account of the fund is updated.");
            Assert.AreEqual(50, clt.GetTransactions()[1].FromAmount, "Testing that the old amount in the account of the fund is updated.");
            Assert.AreEqual(TransType.Buy, clt.GetTransactions()[1].TransactionType, "Testing that the transaction type is a buy.");
        }

        //[TestMethod]
        [TestCase]
        public void TestBuyFX()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(1).Key;

            //Fund currency is USD
            clt.BuyFund(Isin, 10);

            ExchangeManager em = new ExchangeManager();

            decimal convAmount = em.ConvertValue(Currency.USD, Currency.EUR, 10);

            Assert.AreEqual(50 - convAmount, clt.GetAccounts()[0].Balance);
        }

        //[TestMethod]
        [TestCase]
        public void TestBuyTransactionsFX()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            //Fund currency is USD
            string Isin = FundManager.Funds.ElementAt(1).Key;

            clt.BuyFund(Isin, 10);

            ExchangeManager em = new ExchangeManager();

            decimal convAmount = em.ConvertValue(Currency.USD, Currency.EUR, 10);


            Assert.AreEqual(2, clt.GetTransactions().Length, "Testing number of transactions is 2, one CT and one Buy.");
            Assert.AreEqual(Isin, clt.GetTransactions()[1].Fund.ISIN, "Testing that the added ISIN is correct.");
            Assert.AreEqual(50 - convAmount, clt.GetTransactions()[1].NewAmount, "Testing that the new amount in the account of the fund is updated.");
            Assert.AreEqual(50, clt.GetTransactions()[1].FromAmount, "Testing that the old amount in the account of the fund is updated.");
            Assert.AreEqual(TransType.Buy, clt.GetTransactions()[1].TransactionType, "Testing that the transaction type is a buy.");
        }

        //[TestMethod]
        [TestCase]
        public void TestPartialSell()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 50);
            clt.SellFund(Isin, 10);

            Assert.AreEqual(1, clt.GetFunds().Length);
            Assert.AreEqual(Isin, ((Fund)clt.GetFunds().ElementAt(0).Key).ISIN);
            Assert.AreEqual(40, clt.GetFunds().ElementAt(0).Value);

        }

        //[TestMethod]
        [TestCase]
        public void TestSellAll()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 50);
            clt.SellFund(Isin, 50);

            Assert.AreEqual(0, clt.GetFunds().Length);
        }

        //[TestMethod]
        [TestCase]
        public void TestSellOverMultipleAccounts()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);
            clt.AddBalanceToAccount(Currency.USD, 30);

            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 60);

            ExchangeManager em = new ExchangeManager();
            //decimal convAmount = em.ConvertValue(Currency.USD,)

            Assert.AreEqual(1, clt.GetFunds().Length);
            Assert.AreEqual(Isin, ((Fund)clt.GetFunds().ElementAt(0).Key).ISIN);
            Assert.AreEqual(60, clt.GetFunds().ElementAt(0).Value);

        }


        //[TestMethod]
        [TestCase]
        public void TestSellTransactions()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 50);
            clt.SellFund(Isin, 10);

            Assert.AreEqual(3, clt.GetTransactions().Length, "Testing number of transactions is 3, one CT and one Buy and one Sell.");
            Assert.AreEqual(Isin, clt.GetTransactions()[2].Fund.ISIN, "Testing that the added ISIN is correct.");
            Assert.AreEqual(10, clt.GetTransactions()[2].NewAmount, "Testing that the new amount in the account of the fund is updated.");
            Assert.AreEqual(0, clt.GetTransactions()[2].FromAmount, "Testing that the old amount in the account of the fund is updated.");
            Assert.AreEqual(TransType.Sell, clt.GetTransactions()[2].TransactionType, "Testing that the transaction type is a sell.");
        }

        //[TestMethod]
        [TestCase]
        public void TestSellTransactionsFX()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 50);

            string Isin = FundManager.Funds.ElementAt(1).Key;

            clt.BuyFund(Isin, 20);
            clt.SellFund(Isin, 10);

            ExchangeManager em = new ExchangeManager();
            decimal convBuyAmount = em.ConvertValue(Currency.USD, Currency.EUR, 20);
            decimal convSellAmount = em.ConvertValue(Currency.USD, Currency.EUR, 10);

            Assert.IsNotNull((from acc in clt.GetAccounts() where acc.Currency == Currency.USD select acc).FirstOrDefault());
            Assert.AreEqual(10, (from acc in clt.GetAccounts() where acc.Currency == Currency.USD select acc.Balance).Sum());
        }


        //[TestMethod]
        [TestCase]
        [ExpectedException(typeof(FundNotOwnedException))]
        public void TestSellTransactionsWithoutBalance()
        {
            Client clt = new Client();
            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.SellFund(Isin, 10);
        }

        //[TestMethod]
        [TestCase]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void TestSellInvalidPartial()
        {
            Client clt = new Client();
            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.AddBalanceToAccount(Currency.EUR, 50);

            clt.BuyFund(Isin, 30);
            clt.SellFund(Isin, 50);
        }

        //[TestMethod]
        [TestCase]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void TestBuyTransactionsWithoutBalance()
        {
            Client clt = new Client();
            string Isin = FundManager.Funds.ElementAt(0).Key;

            clt.BuyFund(Isin, 10);
        }

        //[TestMethod]
        [TestCase]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void TestInvalidCashDisbursement()
        {
            Client clt = new Client();

            clt.AddBalanceToAccount(Currency.EUR, -10);
        }


        //[TestMethod]
        [TestCase]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void TestInvalidCashDisbursementWithBalance()
        {
            Client clt = new Client();
            clt.AddBalanceToAccount(Currency.EUR, 10);
            clt.AddBalanceToAccount(Currency.EUR, 10);

            clt.AddBalanceToAccount(Currency.EUR, -30);

        }



    }
}
