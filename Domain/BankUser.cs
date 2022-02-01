using System;
using System.Linq;

namespace Domain;

public class BankUser
{
    public string Number { get; set; }
    public int Pin { get; set; }
    public Account Account { get; set; }

    public BankUser(string number, int pin)
    {
        Number = number;
        Pin = pin;
        Account = new();
    }
}
