using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject;
using NUnit.Framework;

namespace FundPlatform.Tests
{
    //[TestClass]
    public class TestFundManager
    {
        //[TestMethod]
        [TestCase]
        public void TestBuyFund()
        {
            Client clt = new Client();
            FundManager fm = new FundManager();

            string Isin = FundManager.Funds.ElementAt(0).Key;

            AccountManager am = new AccountManager();
            am.CashTransfer(clt, Currency.EUR, 50);
            fm.BuyFund(clt, Isin, 50);

            Assert.AreEqual(1, clt.GetFunds().Length);
        }

        //[TestMethod]
        [TestCase]
        public void TestSellFund()
        {
            Client clt = new Client();
            FundManager fm = new FundManager();

            string Isin = FundManager.Funds.ElementAt(0).Key;

            AccountManager am = new AccountManager();
            am.CashTransfer(clt, Currency.EUR, 50);
            fm.BuyFund(clt, Isin, 50);
            fm.SellFund(clt, Isin, 50);

            Assert.AreEqual(0, clt.GetFunds().Length);
        }

    }
}
