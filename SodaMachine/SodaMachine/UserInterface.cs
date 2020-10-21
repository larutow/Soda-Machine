using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    static class UserInterface
    {
        private static string topMenu;

        public static void MainDisplay(SodaMachine sodaMachine, List<Coin> coinsEntered)
        {
            Console.WriteLine("Vending machine has the following options:");
            Console.WriteLine($"1. Cola \n2. Orange Soda \n3. Rootbeer");
            Console.WriteLine($"Money entered to machine: {sodaMachine.CalcSum(coinsEntered)}");
            Console.WriteLine($"Enter corresponding number of desired soda");
            int selection = ValidSodaEntry();

        }

        public static int ValidSodaEntry()
        {
            int numSelect = 0;
            bool isUserEntryInt = false;
            do
            {
                isUserEntryInt = int.TryParse(Console.ReadLine(), out numSelect);
                if (!isUserEntryInt || (numSelect < 1 || numSelect > 3))
                {
                    Console.WriteLine("Invalid entry, enter a number 1 through 3");
                }
            } while (numSelect < 1 || numSelect > 3);

            return numSelect
        }


    }
}
