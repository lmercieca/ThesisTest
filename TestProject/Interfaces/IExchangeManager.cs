using System;
namespace TestProject
{
    interface IExchangeManager
    {
        decimal ConvertValue(Currency from, Currency to, decimal amount);
    }
}
