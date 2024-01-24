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
    public class Topping
    {
        private string type;
        
        public string Type { get; set; }

        public Topping() { }

        public Topping(string t)
        {
            Type = t;
        }

        public override string ToString()
        {
            return $"Type: {Type}";
        }
    }
}
