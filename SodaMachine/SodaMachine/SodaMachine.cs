using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        public List<Coin> register;
        public List<Can> inventory;

        public SodaMachine()
        {

        }

        public void UseMachine(List<Coin> moneyIn, Can desiredSoda)
        {

            /*
             * If not enough money is passed in, don’t complete transaction and give the money back
             * If exact change is passed in, accept payment and dispense a soda instance that gets saved in my Backpack.
             * If too much money is passed in, accept the payment, return change as a list of coins from internal, limited register, and dispense a soda instance that gets saved to my Backpack.
             * If too much money is passed in but there isn’t sufficient change in the machine’s internal register, don’t complete transaction: give the money back.
             * If exact or too much money is passed in but there isn’t sufficient inventory for that soda, don’t complete the transaction: give the money back.
             */

            //sum money in
            double sumInput = 0;
            foreach (Coin coin in moneyIn)
            {
                sumInput += coin.Value;
            }
            //display to UI

            //If not enough money is passed in, don’t complete transaction and give the money back
            if (sumInput < desiredSoda.Value)
            {
                // return change, don't complete tx
            }

            if (sumInput >= desiredSoda.Value && inventory.Contains(desiredSoda)){
                //give soda to customer
                //remove soda from inventory
                //return change if not there
            }





        }

        private Can distributeCan()
        {
            int x = 0;

            return inventory[x];
        }
    }
}
