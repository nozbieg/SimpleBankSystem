using System;
using System.Linq;

namespace App
{
    public interface IBankSystem
    {
        void PrintBaseMenu();
        void PrintWelocmeScreen();
        void TakeBaseInput();
        void TakeLoggedInput();
    }
}
