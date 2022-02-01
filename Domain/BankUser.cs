using System;
using System.Linq;

namespace Domain;

public class BankUser
{
    public string Number { get; set; }
    public string Pin { get; set; }
    public double AccountBalance { get; set; }

    public BankUser(string number, string pin)
    {
        Number = number;
        Pin = pin;
        AccountBalance = 0;
    }
}
