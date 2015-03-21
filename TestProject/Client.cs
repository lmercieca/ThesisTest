using System.Collections.Generic;
using System.Linq;

namespace TestProject
{
    public class Client : TestProject.IClient
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        private Dictionary<Currency, Account> Accounts { get; set; }
        private Dictionary<Fund, decimal> Funds { get; set; }
        private List<Transaction> TransactionHistory { get; set; }

        public Client()
        {
            TransactionHistory = new List<Transaction>();
            Funds = new Dictionary<Fund, decimal>();
            Accounts = new Dictionary<Currency, Account>();
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account.Currency, account);
        }

        public Account[] GetAccounts()
        {          
                return Accounts.Values.ToArray();
        }

        public Transaction[] GetTransactions()
        {
            return TransactionHistory.ToArray();
        }

        public KeyValuePair<Fund, decimal>[] GetFunds()
        {
            return Funds.ToArray();
        }

        public void CloseAccount(Currency currency)
        {
            Accounts.Remove(currency);
        }

        public bool HasAccountInCurrency(Currency currency)
        {
            return Accounts.Keys.Contains(currency);
        }

        public void AddBalanceToAccount(Currency currency, decimal amount, bool addTransactionRecord = true)
        {
            if (HasAccountInCurrency(currency))
            {
                decimal balance = Accounts[currency].Balance;

                if (Accounts[currency].Balance + amount < 0)
                    throw new InsufficientFundsException(amount, balance);

                Account acct = Accounts.Where(x => x.Key == currency).First().Value;

                if (addTransactionRecord)
                {
                    if (amount > 0)
                        TransactionHistory.Add(new Transaction(TransType.CT, acct.Balance + amount, acct.Balance, null, currency));
                    else
                        TransactionHistory.Add(new Transaction(TransType.CD, acct.Balance + amount, acct.Balance, null, currency));
                }

                acct.AddBalance(amount);

                if (Accounts[currency].Balance == 0)
                {
                    CloseAccount(currency);
                }
            }
            else
            {
                if (amount < 0)
                    throw new InsufficientFundsException(amount,0);

                if (addTransactionRecord)
                {
                    TransactionHistory.Add(new Transaction(TransType.CT, amount, 0, null, currency));
                }

                AddAccount(new Account(currency, amount));
            }
        }

        public void BuyFund(string ISIN, decimal amount)
        {

            Fund fnd = new FundManager().GetFundByISIN(ISIN);
            decimal originalAmount = amount;

            if (fnd != null)
            {
                if (Accounts.ContainsKey(fnd.Currency) && Accounts[fnd.Currency].Balance >= amount)
                {
                    TransactionHistory.Add(new Transaction(TransType.Buy, Accounts[fnd.Currency].Balance - amount, Accounts[fnd.Currency].Balance, fnd, Accounts[fnd.Currency].Currency));

                    Funds.Add(fnd, amount);
                    Accounts[fnd.Currency].ReduceBalance(amount);

                    if (Accounts[fnd.Currency].Balance == 0)
                    {
                        CloseAccount(fnd.Currency);
                    }
                }
                else
                {
                    ExchangeManager em = new ExchangeManager();

                    decimal totalConvertedAmount = (from amt in Accounts select em.ConvertValue(amt.Key, fnd.Currency, Accounts[amt.Key].Balance)).Sum();

                    if (totalConvertedAmount < amount)
                    {
                        throw new InsufficientFundsException(amount, totalConvertedAmount);
                    }
                    else
                    {
                        List<Account> dirtyAccounts = new List<Account>();

                        foreach (Account acc in Accounts.Values)
                        {
                            decimal value = em.ConvertValue(acc.Currency, fnd.Currency, acc.Balance);

                            if (amount > value)
                            {
                                dirtyAccounts.Add(acc);
                                TransactionHistory.Add(new Transaction(TransType.Buy, 0, acc.Balance, fnd, acc.Currency));
                                amount = amount - value;
                            }
                            else
                            {
                                decimal amountForBalance = em.ConvertValue(fnd.Currency, acc.Currency, amount);
                                TransactionHistory.Add(new Transaction(TransType.Buy, acc.Balance - amountForBalance, acc.Balance, fnd, acc.Currency));
                                acc.ReduceBalance(amountForBalance);
                                Funds.Add(fnd, originalAmount);
                                break;
                            }
                        }

                        foreach (Account removeAcct in dirtyAccounts)
                        {
                            CloseAccount(removeAcct.Currency);
                        }
                    }
                }
            }

        }

        public void SellFund(string ISIN, decimal amount)
        {
            // Added change to test affected tests
            Fund fnd = new FundManager().GetFundByISIN(ISIN);
          
            if (fnd != null)
            {
                if (!Funds.ContainsKey(fnd))
                    throw new FundNotOwnedException(fnd);


                if (Funds[fnd] < amount)
                    throw new InsufficientFundsException(amount, Funds[fnd]);
                else
                {

                    Funds[fnd] = Funds[fnd] - amount + 0;
                    AddBalanceToAccount(fnd.Currency, amount, false);

                    TransactionHistory.Add(new Transaction(TransType.Sell, Accounts[fnd.Currency].Balance, Accounts[fnd.Currency].Balance - amount, fnd, fnd.Currency));

                    if (Funds[fnd] == 0)
                        Funds.Remove(fnd);

                }
            }
        }

       

        public static string TestMethod(string enter)
        {
            return "";
        }

        public void  YetAnotherTestMethod(string enter)
        {
        }
    }
}
