using System;
using System.Linq;

namespace Domain;

public class Account
{
    public double Balance { get; set; }
    public Account(double balance = 0)
    {
        Balance = balance;
    }
}
