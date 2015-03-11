using System;
namespace TestProject
{
    interface IAccount
    {
        void AddBalance(decimal amount);
        decimal Balance { get; }
        Currency Currency { get; }
        void ReduceBalance(decimal amount);
    }
}
