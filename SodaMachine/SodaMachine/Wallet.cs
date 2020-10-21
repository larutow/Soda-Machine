using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        public List<Coin> coins;
        public Card card;

        public Wallet()
        {
            coins = new List<Coin>();
            StartingCoins();
            card = new Card();

        }

        private void StartingCoins()
        {
            //+ $4.92
            for (int i = 0; i < 12; i++)
            {
                coins.Add(new Quarter());
                coins.Add(new Dime());
                coins.Add(new Nickel());
                coins.Add(new Penny());
            }

            //+ $0.08
            coins.Add(new Nickel());
            coins.Add(new Penny());
            coins.Add(new Penny());
            coins.Add(new Penny());
        }

    }
}
