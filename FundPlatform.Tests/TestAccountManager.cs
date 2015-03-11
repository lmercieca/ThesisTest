using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestProject;

using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;

namespace FundPlatform.Tests
{
    [TestClass]
    public class TestAccountManager
    {
        [TestMethod]
        //[TestCase]
        public void TestCashTransfer()
        {
            AccountManager am = new AccountManager();            
            Client clt = new Client();
            am.CashTransfer(clt, Currency.EUR, 50);

            Assert.AreEqual(1, clt.GetAccounts().Length);
            Assert.AreEqual(Currency.EUR, clt.GetAccounts()[0].Currency);
            Assert.AreEqual(50, clt.GetAccounts()[0].Balance);
        }

        [TestMethod]
        //[TestCase]
        public void TestCashDisbursement()
        {
            AccountManager am = new AccountManager();
            Client clt = new Client();

            am.CashTransfer(clt, Currency.EUR, 50);
            am.CashDisbursement(clt, Currency.EUR, 10);


            Assert.AreEqual(1, clt.GetAccounts().Length);
            Assert.AreEqual(Currency.EUR, clt.GetAccounts()[0].Currency);
            Assert.AreEqual(40, clt.GetAccounts()[0].Balance);
        }
    }
}
