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

        public void PerformTransaction()
        {
            //test
            sodaMachine.UseMachine(customer.wallet.coins, sodaMachine.inventory[0], customer.backpack);

            Console.ReadLine();
        }
    }
}
