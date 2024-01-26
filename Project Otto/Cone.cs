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
    public class Cone : IceCream
    {
        private bool dipped;
        public bool Dipped { get; set; }

        public Cone() { }
        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d)
        {
            Option = o;
            Scoops = s;
            Flavours = f;
            Toppings = t;
            Dipped = d;
        }

        public override double CalculatePrice()
        {
            return 0; /* Placeholder, not implemented yet */
        }

        public override string ToString()
        {
            return base.ToString() + $"  Dipped: {Dipped}";
        }
    }
}
