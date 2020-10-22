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
            List<Coin> changeReturn = new List<Coin>();
            do
            {
                UserInterface.VendingDisplay(sodaMachine, coinsEntered);
                
                switch (UserInterface.UserMenu())
                {
                    case 1:
                        coinsEntered = UserInterface.CoinSelectScreen(customer.wallet.coins);
                        break;
                    case 2:
                        Can desiredItem = UserInterface.DrinkPurchaseScreen(customer, sodaMachine);
                        changeReturn = sodaMachine.UseMachine(coinsEntered, desiredItem, customer.backpack);
                        break;
                }
                

                



                sodaMachine.UseMachine(coinsEntered, sodaMachine.inventory[0], customer.backpack);

                Console.ReadLine();
            }
        }

        public void UserMenu()
        {

        }
    }
}
