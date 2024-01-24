//==========================================================
// Student Number : S10258408
// Student Name : Noel Ng
// Partner Name : -
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Otto
{
    public class Cup : IceCream
    {
        public Cup() { }
        public Cup(string o, int s, List<Flavour> f, List<Topping> t)
        {
            Option = o;
            Scoops = s;
            Flavours = f;
            Toppings = t;
        }

        public override double CalculatePrice()
        {
            return 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
