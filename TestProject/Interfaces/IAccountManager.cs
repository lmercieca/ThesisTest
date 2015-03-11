using System;
namespace TestProject
{
    interface IAccountManager
    {
        void CashDisbursement(Client client, Currency currency, decimal amount);
        void CashTransfer(Client client, Currency currency, decimal amount);
    }
}
