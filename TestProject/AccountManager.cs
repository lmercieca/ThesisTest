using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    public class AccountManager : TestProject.IAccountManager
    {
        public void CashTransfer(Client client, Currency currency, decimal amount)
        {
            client.AddBalanceToAccount(currency, amount);
        }

        public void CashDisbursement(Client client, Currency currency, decimal amount)
        {
            int i = 0;
            amount = amount * -1;
            client.AddBalanceToAccount(currency, amount);
        }
    }
}
