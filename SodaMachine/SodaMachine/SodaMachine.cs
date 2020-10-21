using System;
using System.CodeDom;
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
            PopulateRegister();
            PopulateInventory();
        }

        private void PopulateRegister()
        {
            for (int i = 0; i < 20; i++)
            {
                register.Add(new Quarter());
                register.Add(new Nickel());
            }

            for(int i = 0; i < 10; i++)
            {
                register.Add(new Dime());
            }

            for (int i = 0; i < 50; i++)
            {
                register.Add(new Penny());
            }
        }

        private void PopulateInventory()
        {
            for(int i = 0; i < 10; i++)
            {
                inventory.Add(new Cola());
                inventory.Add(new OrangeSoda());
                inventory.Add(new RootBeer());
            }
        }

        public List<Coin> UseMachine(List<Coin> moneyIn, Can desiredSoda, Backpack userBackpack)
        {

            /*
             * If not enough money is passed in, don’t complete transaction and give the money back
             * If exact change is passed in, accept payment and dispense a soda instance that gets saved in my Backpack.
             * If too much money is passed in, accept the payment, return change as a list of coins from internal, limited register, and dispense a soda instance that gets saved to my Backpack.
             * If too much money is passed in but there isn’t sufficient change in the machine’s internal register, don’t complete transaction: give the money back.
             * If exact or too much money is passed in but there isn’t sufficient inventory for that soda, don’t complete the transaction: give the money back.
             */

            
            double sumInputValue = 0;
            double registerChangeValue = 0;
            double calculatedChangeValue;
            List<Coin> returnChange = new List<Coin>();

            //sum money input
            //sumInputValue = CalcSum(moneyIn); // add to register?
            foreach (Coin coin in moneyIn)
            {
                sumInputValue += coin.Value;
                register.Add(coin);
            }

            //sum money in register
            registerChangeValue = CalcSum(register);
            
            //display to UI

            //If not enough money is passed in, don’t complete transaction and give the money back
            if (sumInputValue < desiredSoda.Value)
            {
                // return change, don't complete tx
                returnChange = moneyIn;
                
            }

            // * If exact change is passed in, accept payment and dispense a soda instance that gets saved in my Backpack.
            if (sumInputValue == desiredSoda.Value && inventory.Contains(desiredSoda))
            {
                //give soda to customer
                //remove soda from inventory
                userBackpack.cans.Add(desiredSoda);
                inventory.Remove(desiredSoda);
            }
            else 
            //*If too much money is passed in, accept the payment, return change as a list of coins from internal, limited register, and dispense a soda instance that gets saved to my Backpack.
            if (sumInputValue > desiredSoda.Value && inventory.Contains(desiredSoda))
            {
                //check internal register
                calculatedChangeValue = sumInputValue - desiredSoda.Value;
                if(ChangeAlgo(calculatedChangeValue, out List<Coin> tryReturnChange))
                {
                    return tryReturnChange;
                }

                if (calculatedChangeValue > registerChangeValue)
                {
                    //not enough change, give money back
                    return moneyIn;
                }

                ChangeAlgo(calculatedChangeValue, out returnChange);
                
            }

            return returnChange;
        }


        // change will exist
        // change is diff of input & soda cost
        // register can either
            // 1 not have enough (total val register < total change (coinlist) needed OR register has enough value BUT not enough individual coins to give accurate change, give back input money)
            // 2 have just enough coins to give change (total val register == total change needed, give out entire register)
            // 3 have more than enough coins to give change
        // building a coin list algorithmically
        // for each coin in the list
        public double CalcSum(List<Coin> coins)
        {
            double sum = 0;
            foreach(Coin coin in coins)
            {
                sum += coin.Value;
            }

            return sum;
        }

        private bool ChangeAlgo(double targetChangeValue, out List<Coin> returnChange)
        {
            
            //change algorithm
            //tries w/ biggest coins to meet change requirement
            //refactor idea: make generalizeable (type & value
            bool makeChange = false;
            returnChange = new List<Coin>();
            double returnChangeValue = 0;
            
            //List<Coin> returnChange = new List<Coin>;
            
            foreach(Quarter quarter in register)
            {
               if((targetChangeValue - returnChangeValue) >= quarter.Value)
               {
                    returnChange.Add(quarter);
                    returnChangeValue = CalcSum(returnChange);
               }
               
               if (returnChangeValue == targetChangeValue)
               {
                    makeChange = true;
                    break;
               }
            }

            foreach(Dime dime in register)
            {
                returnChange.Add(dime);
                if((targetChangeValue - returnChangeValue)>= 0.10)
                {
                    returnChange.Add(dime);
                    returnChangeValue = CalcSum(returnChange);
                    
                }
                if (returnChangeValue == targetChangeValue)
                {
                    makeChange = true;
                    break;
                }
                
            }

            foreach (Nickel nickel in register)
            {
                returnChange.Add(nickel);
                if ((targetChangeValue - returnChangeValue) >= 0.05)
                {
                    returnChange.Add(nickel);
                    returnChangeValue = CalcSum(returnChange);
                    
                }
                if (returnChangeValue == targetChangeValue)
                {
                    makeChange = true;
                    break;
                }

            }

            foreach (Penny penny in register)
            {
                returnChange.Add(penny);
                if ((targetChangeValue - returnChangeValue) >= 0.01)
                {
                    returnChange.Add(penny);
                    returnChangeValue = CalcSum(returnChange);
                    
                }
                if (returnChangeValue == targetChangeValue)

                {
                    makeChange = true;
                    break;
                }

            }

            if (makeChange)
            {
                foreach(Coin coin in returnChange)
                {
                    register.Remove(coin);
                }
            }
            else
            {
                returnChange.Clear();
            }

            return makeChange;
        }

        private Can distributeCan()
        {
            int x = 0;

            return inventory[x];
        }
    }
}
