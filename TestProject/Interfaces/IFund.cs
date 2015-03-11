using System;
namespace TestProject
{
    interface IFund
    {
        Currency Currency { get; set; }
        string FundName { get; set; }
        string ISIN { get; set; }
        decimal NAV { get; set; }
    }
}
