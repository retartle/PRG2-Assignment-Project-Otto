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
    public class Waffle : IceCream
    {
        private string waffleFlavour;
        public string WaffleFlavour { get; set; }

        public Waffle() { }
        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string wf)
        {
            Option = o;
            Scoops = s;
            Flavours = f;
            Toppings = t;
            WaffleFlavour = wf;
        }

        public override double CalculatePrice()
        {
            return 0; /* Placeholder, not implemented yet */
        }

        public override string ToString()
        {
            return base.ToString() + $"  Waffle Flavour: {WaffleFlavour}";
        }
    }
}
