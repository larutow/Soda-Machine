using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    abstract class Coin
    {
        public double Value
        {
            get
            {
                return value;
            }
        }

        protected double value;
        public string name;

        public Coin()
        {
            
            
        }
    }
}
