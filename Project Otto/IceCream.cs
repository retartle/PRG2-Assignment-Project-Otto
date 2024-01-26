//==========================================================
// Student Number : S10258408
// Student Name : Noel Ng
// Partner Name : -
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project_Otto
{
    public abstract class IceCream
    {
        private string option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;

        public string Option { get; set; }
        public int Scoops { get; set; }
        public List<Flavour> Flavours { get; set; } 
        public List<Topping> Toppings { get; set; }

        public IceCream() { }

        public IceCream(string o, int s, List<Flavour> f, List<Topping> t)
        {
            option = o;
            scoops = s;
            Flavours = new List<Flavour>(f);
            Toppings = new List<Topping>(t);
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            return $"Option: {Option}  Scoops: {Scoops}";
        }
    }
}
