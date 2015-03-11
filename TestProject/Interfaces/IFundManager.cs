using System;
namespace TestProject
{
    interface IFundManager
    {
        void BuyFund(Client client, string ISIN, decimal amount);
        Fund GetFundByISIN(string ISIN);
        void SellFund(Client client, string ISIN, decimal amount);
    }
}
