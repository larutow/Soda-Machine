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
            for (int i = 0; i < 15; i++)
            {
                inventory.Add(new Cola(0.35));
                inventory.Add(new OrangeSoda(0.06));
                inventory.Add(new RootBeer(0.60));
            }
        }



        private bool IsDesiredSodaAvailable(string desiredSodaName)
        {
            foreach (Can can in inventory)
            {
                if (can.name == desiredSodaName)
                {
                    return true;
                }
            }
            return false;
        }

        private Can SetDesiredSoda(string desiredSodaName)
        {
            Can desiredCan = new Cola(0);
            foreach (Can can in inventory)
            {
                if (can.name == desiredSodaName)
                {
                    desiredCan = can;
                }
            }
            return desiredCan;
        }

        public List<Coin> UseMachine(List<Coin> moneyIn, string desiredSodaName, Backpack userBackpack)
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
            Can desiredSoda;

            foreach (Coin coin in moneyIn)
            {
                sumInputValue += coin.Value;
                register.Add(coin);
            }
            sumInputValue = Math.Round(sumInputValue, 2);


            if(IsDesiredSodaAvailable(desiredSodaName)){
                desiredSoda = SetDesiredSoda(desiredSodaName);
            }
            else
            {
                UserInterface.NoInventoryAlert();
                return moneyIn;
            }

            //If not enough money is passed in, don’t complete transaction and give the money back
            if (sumInputValue < desiredSoda.Value)
            {
                UserInterface.NotEnoughMoneyAlert();
                return moneyIn;
            }

            // * If exact change is passed in, accept payment and dispense a soda instance that gets saved in my Backpack.
            if (sumInputValue == desiredSoda.Value && inventory.Contains(desiredSoda))
            {
                //give soda to customer
                //remove soda from inventory
                userBackpack.cans.Add(desiredSoda);
                inventory.Remove(desiredSoda);
                UserInterface.CanDelivered(desiredSoda);
                return returnChange;
            }
            else
            //*If too much money is passed in, accept the payment, return change as a list of coins from internal, limited register, and dispense a soda instance that gets saved to my Backpack.
            if (sumInputValue > desiredSoda.Value && InventoryCheck(desiredSoda))
            {

                double registerChangeValue = Math.Round(CalcSum(register), 2);
                calculatedChangeValue = Math.Round((sumInputValue - desiredSoda.Value), 2);

                if (calculatedChangeValue > registerChangeValue)
                {
                    //not enough change, give money back
                    return moneyIn;
                }
                else
                {
                    if (ChangeAlgo(calculatedChangeValue, out List<Coin> tryReturnChange))
                    {
                        userBackpack.cans.Add(desiredSoda);
                        inventory.Remove(desiredSoda);
                        UserInterface.CanDelivered(desiredSoda);
                        return tryReturnChange;
                    }
                    else
                    {
                        //unable to make change 
                        return moneyIn;
                    }
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

        private bool[] RegisterHasCoins()
        {
            bool[] hasCoins = { false, false, false, false };

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
            List<Coin> targetChange = new List<Coin>();
            double idealTargetChangeSum = 0;
            returnChange = new List<Coin>();

            int timeout = 0;
            bool[] hasCoins = RegisterHasCoins();

            //Use USD greedy method (first quarters, then dimes etc.) to build ideal change list 
            do
            {
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.25 && hasCoins[0])
                {
                    targetChange.Add(new Quarter());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.25, 2);
                    RegisterRemoveCoin("quarter");
                    hasCoins = RegisterHasCoins();
                }
                else
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.1 && hasCoins[1])
                {
                    targetChange.Add(new Dime());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.1, 2 );
                    RegisterRemoveCoin("dime");
                    hasCoins = RegisterHasCoins();
                }
                else
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.05 && hasCoins[2])
                {
                    targetChange.Add(new Nickel());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.05, 2);
                    RegisterRemoveCoin("nickel");
                    hasCoins = RegisterHasCoins();
                }
                else
                if (Math.Round((targetChangeValue - idealTargetChangeSum),2) >= 0.01 && hasCoins[3])
                {
                    targetChange.Add(new Penny());
                    idealTargetChangeSum = Math.Round(idealTargetChangeSum += 0.01, 2);
                    RegisterRemoveCoin("penny");
                    hasCoins = RegisterHasCoins();
                }

                timeout ++;

            } while (idealTargetChangeSum < targetChangeValue || timeout < 1000);
            // above generates "targetChange" as a set of coins needed to return change
            // if reigster contains all target change set = set bool = true, target change = return change
            // else, n/a

            if (targetChangeValue == idealTargetChangeSum)
            {
                //change returned is acceptable
                //remove each targetchange from 
                returnChange = targetChange;
                UserInterface.MakeChangeMessage(returnChange, true);
                return true;
            }
            else
            {
                foreach(Coin coin in targetChange)
                {
                    register.Add(coin);
                }
                returnChange = new List<Coin>();
                UserInterface.MakeChangeMessage(returnChange, false);
                return false;
            }
        }
    }
}
