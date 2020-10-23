using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Simulation
    {
        public SodaMachine sodaMachine;
        public Customer customer;

        public Simulation()
        {
            customer = new Customer();
            sodaMachine = new SodaMachine();
        }

        public void RunInstance()
        {
            //test
            //You approach a vending machine
            //[ Vending Machine Display ]
            //User Actions
            // insert coins
            // purchase drink
            // return coins
            // Quit/walk away
            List<Coin> coinsEntered = new List<Coin>();
            List<Coin> changeReturn;
            bool stayatmachine = true;
            do
            {
                Console.Clear();
                UserInterface.VendingDisplay(sodaMachine, coinsEntered);

                switch (UserInterface.UserMenu())
                {
                    case 1:
                        UserInterface.CoinSelectScreen();
                        coinsEntered.AddRange(customer.SelectCoins());
                        break;
                    case 2:
                        UserInterface.DrinkPurchaseScreen();
                        string desiredItem = customer.SelectCan();
                        changeReturn = sodaMachine.UseMachine(coinsEntered, desiredItem, customer.backpack);
                        coinsEntered = changeReturn;
                        break;
                    case 3:
                        customer.ReturnCoins(coinsEntered);
                        coinsEntered = new List<Coin>();
                        break;
                    case 4:
                        customer.CheckWallet();
                        break;
                    case 5:
                        customer.CheckBackpack();
                        break;
                    case 6:
                        stayatmachine = false;
                        break;
                }


            } while (stayatmachine);
        }
    }
}
