using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    static class UserInterface
    {


        public static void VendingDisplay(SodaMachine sodaMachine, List<Coin> coinsEntered)
        {
            Console.WriteLine("Vending machine has the following options:");
            Console.WriteLine($"Cola - Orange Soda - Rootbeer");
            Console.WriteLine($"\nMoney entered to machine: {Math.Round(sodaMachine.CalcSum(coinsEntered),2)}");

        }

        public static int UserMenu()
        {
            Console.WriteLine("\nUser Options:");
            Console.WriteLine("1. Insert coins");
            Console.WriteLine("2. Choose a soda option");
            Console.WriteLine("3. Quit");
            return IntValidEntry(3);
        }

        public static List<Coin> CoinSelectScreen(List<Coin> usersCoins, List<Coin> coinsInMachine)
        {
            List<Coin> selectedCoins;
            List<int> changeCount = DisplayCoins(usersCoins);
            Console.WriteLine("Please enter how many coins you'd like to insert.\nWe'll ask you for quarters, nickels, dimes, and pennies - in that order.");
            selectedCoins = EnterQtyCoins(usersCoins, changeCount);
            coinsInMachine = AddCoins(selectedCoins, coinsInMachine);
            return coinsInMachine;
        }

        private static List<Coin> AddCoins(List<Coin> selectedCoins, List<Coin> coinsInMachine)
        {
            foreach(Coin coin in selectedCoins)
            {
                coinsInMachine.Add(coin);
            }
            return coinsInMachine;
        }

        private static List<Coin> EnterQtyCoins(List<Coin> usersCoins, List<int> coinsAvailable)
        {
            List<Coin> desiredCoins = new List<Coin>();
            int[] numberOfEachToInsert = { 0, 0, 0, 0 };
            List<string> coinNames = new List<string> { "quarters", "dimes", "nickels", "pennies" };
            bool parseSuccess = false;

            for (int i = 0; i < coinNames.Count; i++)
            {
                Console.WriteLine($"Enter # of {coinNames[i]} - you have {coinsAvailable[i]} available in your wallet");
                parseSuccess = false;
                do
                {
                    parseSuccess = int.TryParse(Console.ReadLine(), out numberOfEachToInsert[i]);
                    if (!parseSuccess)
                    {
                        Console.WriteLine("Please enter a valid integer value");
                    }
                    if (numberOfEachToInsert[i] > coinsAvailable[i] || numberOfEachToInsert[i] < 0)
                    {
                        Console.WriteLine($"Please enter a number between 0 and {coinsAvailable[i]}");
                    }
                } while (!parseSuccess || numberOfEachToInsert[i] < 0 || numberOfEachToInsert[i] > coinsAvailable[i]);
            }

            for(int i = 0; i < numberOfEachToInsert[0]; i++)
            {
                
                Coin coinFromWallet = usersCoins.Find(x => x.name == "quarter");
                desiredCoins.Add(coinFromWallet);
                usersCoins.Remove(coinFromWallet);
            }
            for (int i = 0; i < numberOfEachToInsert[1]; i++)
            {
                desiredCoins.Add(new Dime());
                usersCoins.Remove(usersCoins.Find(x => x.name == "dime"));
            }
            for (int i = 0; i < numberOfEachToInsert[2]; i++)
            {
                desiredCoins.Add(new Nickel());
                usersCoins.Remove(usersCoins.Find(x => x.name == "nickel"));
            }
            for (int i = 0; i < numberOfEachToInsert[3]; i++)
            {
                desiredCoins.Add(new Penny());
                usersCoins.Remove(usersCoins.Find(x => x.name == "penny"));
            }

            return desiredCoins;
        }

        private static List<int> DisplayCoins(List<Coin> coins)
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
            Console.WriteLine("You have the following coins in your wallet:");
            Console.WriteLine($"{changeCount[0]} quarters, {changeCount[1]} dimes, {changeCount[2]} nickels, and {changeCount[3]} pennies");
            return changeCount;
        }

        public static Can DrinkPurchaseScreen(Customer customer, SodaMachine sodaMachine)
        {
            Can desiredDrink = new Cola(0.35);

            return desiredDrink;
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
