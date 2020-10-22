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

        public static void CoinSelectScreen()
        {
            Console.WriteLine("Please enter how many coins you'd like to insert.\nWe'll ask you for quarters, nickels, dimes, and pennies - in that order.");
        }

        public static void DrinkPurchaseScreen()
        {
            Console.WriteLine("Please enter the type of drink you'd like to purchase");
            Console.WriteLine("1. Cola\n2.Orange Soda\n3. Rootbeer");
        }

        public static int CanDesiredPrompt()
        {
            int selectionNumber = IntValidEntry(3);
            return selectionNumber;
        }
        public static int[] CoinQtyPrompt(List<int> coinsAvailable)
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

            return numberOfEachToInsert;
        }

        public static void MakeChangeMessage(List<Coin> returnChange)
        {
            if(returnChange.Count == 0)
            {
                Console.WriteLine("No change able to be given");
            }
        }

        public static void MakeChangeMessage(List<Coin> returnChange, double changeValue)
        {
            Console.WriteLine($"You recieved ${changeValue} in change");
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
                    Console.WriteLine($"Invalid entry, enter a number 1 through {options}");
                }
            } while (numSelect < 1 || numSelect > options);

            return numSelect;
        }


    }
}
