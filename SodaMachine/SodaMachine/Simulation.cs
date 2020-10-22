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
            
            do
            {
                UserInterface.VendingDisplay(sodaMachine, coinsEntered);
                
                switch (UserInterface.UserMenu())
                {
                    case 1:
                        UserInterface.CoinSelectScreen();
                        coinsEntered = customer.SelectCoins();
                        
                        break;
                    case 2:
                        UserInterface.DrinkPurchaseScreen();
                        Can desiredItem = customer.SelectDrink(sodaMachine);
                        sodaMachine.UseMachine(coinsEntered, desiredItem, customer.backpack);

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
