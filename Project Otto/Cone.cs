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
            //scoops
            double scoopsPrice = 0;
            if (Scoops == 1)
            {
                scoopsPrice = 4;
            }

            else if (Scoops == 2)
            {
                scoopsPrice = 5.50;
            }

            else if (Scoops == 3)
            {
                scoopsPrice = 6.50;
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

            double dippedPrice = 0;
            //dipping
            if (Dipped)
            {
                dippedPrice = 2;
            }

            double totalPrice = scoopsPrice + flavoursPrice + toppingsPrice + dippedPrice;
            return totalPrice;
        }

        public override string ToString()
        {
            return base.ToString() + $"  Dipped: {Dipped}";
        }
    }
}
