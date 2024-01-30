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
            //scoops
            double scoopsPrice = 0;
            if (Scoops == 1)
            {
                scoopsPrice = 7;
            }

            else if (Scoops == 2)
            {
                scoopsPrice = 8.50;
            }

            else if (Scoops == 3)
            {
                scoopsPrice = 9.50;
            }

            //flavours
            double flavoursPrice = 0;
            foreach (Flavour f in Flavours)
            {
                if (f.Premium)
                {
                    flavoursPrice += (2 * f.Quantity);
                }
            }

            //toppings
            double toppingsPrice = 0;
            foreach (Topping t in Toppings)
            {
                toppingsPrice += 1;
            }

            //waffle flavour
            double wafflePrice = 0;
            if (WaffleFlavour != "original")
            {
                wafflePrice += 3;
            }

            double totalPrice = scoopsPrice + flavoursPrice + toppingsPrice + wafflePrice;
            return totalPrice;
        }

        public override string ToString()
        {
            return base.ToString() + $"  Waffle Flavour: {WaffleFlavour}";
        }
    }
}
