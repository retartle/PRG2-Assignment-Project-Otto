using System;
using System.Collections.Generic;
using System.Linq;
//==========================================================
// Student Number : S10258408
// Student Name : Noel Ng
// Partner Name : -
//==========================================================

using System.Text;
using System.Threading.Tasks;

namespace Project_Otto
{
    public class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;

        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        public Flavour() { }

        public Flavour(string t, bool p, int q)
        {
            Type = t;
            Premium = p;
            Quantity = q;
        }

        public override string ToString()
        {
            return $"Type: {Type}  Premium: {Premium}  Quantity: {Quantity}";
        }
    }
}
