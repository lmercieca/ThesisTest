using System;
namespace TestProject
{
    interface IClient
    {
        Account[] GetAccounts();
        string Name { get; set; }
        string Surname { get; set; }
    }
}
