using System;
using System.Collections.Generic;

namespace SodaMachine
{
    class SodaMachine
    {
        public List<Coin> register;
        public List<Can> inventory;

        public SodaMachine()
        {
            register = new List<Coin>();
            inventory = new List<Can>();
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

            for (int i = 0; i < 10; i++)
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
            for (int i = 0; i < 10; i++)
            {
                inventory.Add(new Cola(0.35));
                inventory.Add(new OrangeSoda(0.06));
                inventory.Add(new RootBeer(0.60));
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
            double calculatedChangeValue;
            List<Coin> returnChange = new List<Coin>();

            //sum money input
            //sumInputValue = CalcSum(moneyIn); // add to register?
            foreach (Coin coin in moneyIn)
            {
                sumInputValue += coin.Value;
                register.Add(coin);
            }
            sumInputValue = Math.Round(sumInputValue, 2);

            
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
            if (sumInputValue > desiredSoda.Value && InventoryCheck(desiredSoda))
            {
                //check internal register
                //sum money in register
                double registerChangeValue = Math.Round(CalcSum(register), 2);
                calculatedChangeValue = Math.Round((sumInputValue - desiredSoda.Value), 2);

                if (calculatedChangeValue > registerChangeValue)
                {
                    //not enough change, give money back
                    return moneyIn;
                }
                if (ChangeAlgo(calculatedChangeValue, out List<Coin> tryReturnChange))
                {
                    return tryReturnChange;
                }

            }

            return returnChange;
        }

        public double CalcSum(List<Coin> coins)
        {
            double sum = 0;
            foreach (Coin coin in coins)
            {
                sum += coin.Value;
            }

            return sum;
        }

        private bool InventoryCheck(Can desiredCan)
        {
            bool inStock = false;
            foreach(Can can in inventory)
            {
                if((desiredCan.name.CompareTo(can.name) == 0))
                {
                    inStock = true;
                    break;
                }
            }
            return inStock;
        }

        private bool[] RegisterHasCoins(bool[] hasCoins)
        {
            
            for(int i = 0; i < hasCoins.Length; i++)
            {
                hasCoins[i] = false;
            }

            foreach(Coin coin in register)
            {
                if (!hasCoins[0] && coin.name == "quarter")
                {
                    hasCoins[0] = true;
                }
                if (!hasCoins[1] && coin.name == "dime")
                {
                    hasCoins[1] = true;
                }
                if (!hasCoins[2] && coin.name == "nickel")
                {
                    hasCoins[2] = true;
                }
                if (!hasCoins[3] && coin.name == "penny")
                {
                    hasCoins[3] = true;
                }
                if(hasCoins[0] && hasCoins[1] && hasCoins[2] && hasCoins[3])
                {
                    break;
                }
            }
            return hasCoins;
        }

        public void RegisterRemoveCoin(string coinname)
        {
            foreach(Coin coin in register)
            {
                if(coin.name == coinname)
                {
                    register.Remove(coin);
                    break;
                }
            }
        }

        private bool ChangeAlgo(double targetChangeValue, out List<Coin> returnChange)
        {

            bool makeChange = false;
            List<Coin> targetChange = new List<Coin>();
            double idealTargetChangeSum = 0;
            returnChange = new List<Coin>();
            double returnChangeValue = 0;
            bool[] hasCoins = { false, false, false, false };

            hasCoins = RegisterHasCoins(hasCoins);

            //Use USD greedy method (first quarters, then dimes etc.) to build ideal change list 
            do
            {
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.25 && hasCoins[0])
                {
                    targetChange.Add(new Quarter());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.25, 2);
                    RegisterRemoveCoin("quarter");
                    hasCoins = RegisterHasCoins(hasCoins);
                }
                else
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.1 && hasCoins[1])
                {
                    targetChange.Add(new Dime());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.1, 2 );
                    RegisterRemoveCoin("dime");
                    hasCoins = RegisterHasCoins(hasCoins);
                }
                else
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.05 && hasCoins[2])
                {
                    targetChange.Add(new Nickel());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.05, 2);
                    RegisterRemoveCoin("nickel");
                    hasCoins = RegisterHasCoins(hasCoins);
                }
                else
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.01 && hasCoins[3])
                {
                    targetChange.Add(new Penny());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.01, 2);
                    RegisterRemoveCoin("penny");
                    hasCoins = RegisterHasCoins(hasCoins);
                }

            } while (idealTargetChangeSum < targetChangeValue);
            // above generates "targetChange" as a set of coins needed to return change
            // if reigster contains all target change set = set bool = true, target change = return change
            // else, n/a

            if (targetChangeValue == idealTargetChangeSum)
            {
                //change returned is acceptable
                //remove each targetchange from 
                returnChange = targetChange;
                do
                {

                } while (targetChange.Count > 0);
                UserInterface.MakeChangeMessage(returnChange);
                return true;
            }
            else
            {
                returnChange = new List<Coin>();
                UserInterface.MakeChangeMessage(returnChange);
                return false;
            }


            //int count = targetChange.Count;
            
            

            //if (targetChange.Count == returnChange.Count) //if all coins are found to satisfy ideal coin case
            //{
            //    makeChange = true;
            //}

            //if (makeChange)
            //{
            //    Console.WriteLine("return change successfully obtained from register");
            //}
            //else//if change couldn't be made
            //{
            //    foreach (Coin coin in returnChange)
            //    {
            //        register.Add(coin);//return coins from change return to register
            //    }
            //    returnChange.Clear();//clear change return
            //}

            //return makeChange;


        }

        private Can distributeCan()
        {
            int x = 0;

            return inventory[x];
        }
    }
}
