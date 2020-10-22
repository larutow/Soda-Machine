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

        public static List<Coin> CoinSelectScreen(List<Coin> coins)
        {
            List<Coin> selectedCoins = new List<Coin>();
            List<int> changeCount = DisplayCoins(coins);
            Console.WriteLine("Please enter how many coins you'd like to insert in the following format:\n quarters dimes nickels pennies with 1 space in between each val\n Example: 0 1 1 12 would be 0 quarters, 1 dime, 1 nickel, and 12 pennies");
            Console.WriteLine("Q  D  N  P");
            selectedCoins = EnterQtyCoins(changeCount);
        }

        public static List<Coin> EnterQtyCoins(List<int> coinsAvailable)
        {
            List<Coin> desiredCoins = new List<Coin>();
            string userentry = Console.ReadLine();

            //Try to format Q D N P
            foreach(Char c in userentry)
            {

            }

        }

        public static List<int> DisplayCoins(List<Coin> coins)
        {
            int quarterCount = 0;
            int dimeCount = 0;
            int nickelCount = 0;
            int pennyCount = 0;
            List<int> changeCount = new List<int> { quarterCount, dimeCount, nickelCount, pennyCount };
            foreach (Coin coin in coins)
            {
                if (coin.name == "quarter")
                {
                   changeCount[0]++;
                }
                if (coin.name == "dime")
                {
                    changeCount[1]++;
                }
                if (coin.name == "nickel")
                {
                    changeCount[2]++;
                }
                if (coin.name == "penny")
                {
                    changeCount[3]++;
                }

            }
            Console.WriteLine("You have the following coins:");
            Console.WriteLine($"{changeCount[0]} quarters, {changeCount[1]} dimes, {changeCount[2]} nickels, and {changeCount[3]} pennies");
            return changeCount;
        }

        public static void DrinkPurchaseScreen(Customer customer, SodaMachine sodaMachine)
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
