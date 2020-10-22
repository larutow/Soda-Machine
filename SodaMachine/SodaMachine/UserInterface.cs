using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    static class UserInterface
    {


        public static void VendingDisplay(SodaMachine sodaMachine, List<Coin> coinsEntered)
        {
            Console.WriteLine("Vending machine has the following options:");
            Console.WriteLine($"1. Cola \n2. Orange Soda \n3. Rootbeer");
            Console.WriteLine($"Money entered to machine: {sodaMachine.CalcSum(coinsEntered)}");

        }

        public static int UserMenu()
        {
            Console.WriteLine("User Options:");
            Console.WriteLine("1. Insert coins");
            Console.WriteLine("2. Choose a soda option");
            Console.WriteLine("3. Quit");
            return IntValidEntry(3);
        }

        public static void CoinSelectScreen()
        {

        }

        public static void DrinkPurchaseScreen()
        {

        }

        private static Can PickSoda()
        {
            Can sodaDesired;
            int selection = IntValidEntry(3);
            switch (selection)
            {
                case 1:
                    sodaDesired = new Cola(0.35);
                    break;
                case 2:
                    sodaDesired = new OrangeSoda(0.06);
                    break;
                case 3:
                    sodaDesired = new RootBeer(0.60);
                    break;
                default:
                    sodaDesired = new Cola(0.35);
                    break;
            }
            return sodaDesired;
        }

        private static int IntValidEntry(int options)
        {
            int numSelect = 0;
            bool isUserEntryInt = false;
            do
            {
                isUserEntryInt = int.TryParse(Console.ReadLine(), out numSelect);
                if (!isUserEntryInt || (numSelect < 1 || numSelect > options))
                {
                    Console.WriteLine("Invalid entry, enter a number 1 through 3");
                }
            } while (numSelect < 1 || numSelect > options);

            return numSelect;
        }


    }
}
