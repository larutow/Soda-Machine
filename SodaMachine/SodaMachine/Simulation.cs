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
                        coinsEntered = UserInterface.CoinSelectScreen(customer.wallet.coins, coinsEntered);
                        break;
                    case 2:
                        Can desiredItem = UserInterface.DrinkPurchaseScreen(customer, sodaMachine);
                        changeReturn = sodaMachine.UseMachine(coinsEntered, desiredItem, customer.backpack);
                        break;
                    case 3:

                        break;
                    case 4:
                        stayatmachine = false;
                        break;
                }


            } while (stayatmachine);
        }
    }
}
