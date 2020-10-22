using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        public Wallet wallet;
        public Backpack backpack;

        public Customer()
        {
            wallet = new Wallet();
            backpack = new Backpack();
        }

        public List<Coin> SelectCoins()
        {

            List<Coin> selectedCoins;
            List<int> changeCount = CountCoins(wallet.coins);

            selectedCoins = EnterQtyCoins(wallet.coins, changeCount);
            
            return selectedCoins;
        
        }

        private List<Coin> EnterQtyCoins(List<Coin> usersCoins, List<int> coinsAvailable)
        {
            List<Coin> desiredCoins = new List<Coin>();
            int[] numberOfEachToInsert = { 0, 0, 0, 0 };

            numberOfEachToInsert = UserInterface.CoinQtyPrompt(coinsAvailable);
            
            for (int i = 0; i < numberOfEachToInsert[0]; i++)
            {
                Coin coinFromWallet = usersCoins.Find(x => x.name == "quarter");
                desiredCoins.Add(coinFromWallet);
                usersCoins.Remove(coinFromWallet);
            }
            for (int i = 0; i < numberOfEachToInsert[1]; i++)
            {
                Coin coinFromWallet = usersCoins.Find(x => x.name == "dime");
                desiredCoins.Add(coinFromWallet);
                usersCoins.Remove(coinFromWallet);
            }
            for (int i = 0; i < numberOfEachToInsert[2]; i++)
            {
                Coin coinFromWallet = usersCoins.Find(x => x.name == "nickel");
                desiredCoins.Add(coinFromWallet);
                usersCoins.Remove(coinFromWallet);
            }
            for (int i = 0; i < numberOfEachToInsert[3]; i++)
            {
                Coin coinFromWallet = usersCoins.Find(x => x.name == "penny");
                desiredCoins.Add(coinFromWallet);
                usersCoins.Remove(coinFromWallet);
            }

            return desiredCoins;
        }

        private List<int> CountCoins(List<Coin> coins)
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
            //idscplaycoints
            return changeCount;
        }

        public Can SelectCan()
        {
            Can desiredCan;
            int selectionNumber = UserInterface.CanDesiredPrompt();
            desiredCan = PickSoda(selectionNumber);
            return desiredCan;
        }

        private static Can PickSoda(int selection)
        {
            Can sodaDesired;
            switch (selection)
            {
                case 1:
                    sodaDesired = new Cola();
                    break;
                case 2:
                    sodaDesired = new OrangeSoda();
                    break;
                case 3:
                    sodaDesired = new RootBeer();
                    break;
                default:
                    sodaDesired = new Cola();
                    break;
            }
            return sodaDesired;
        }
    }
}
