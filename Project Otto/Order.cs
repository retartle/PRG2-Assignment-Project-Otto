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
    public class Order
    {
        private int id;
        private DateTime timeReceived;
        private DateTime timeFulfilled;
        private List<IceCream> iceCreamList;

        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();

        public Order() { }

        public Order(int i, DateTime tr)
        {
            Id = i;
            TimeReceived = tr;
        }

        public void ModifyIceCream(int id)
        {
            /* Placeholder, not implemented yet */
        }

        public void AddIceCream(IceCream ic)
        {
            IceCreamList.Add(ic);
        }

        public void DeleteIceCream(int id)
        {
            /* Placeholder, not implemented yet */
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in iceCreamList)
            {
                double price = iceCream.CalculatePrice();
                total += price;
            }
            return total;

            /* Not completed, may be wrong */
        }

        public override string ToString()
        {
            return $"ID: {Id}  Time Received: {TimeFulfilled}  Time Fulfilled: {TimeReceived}";
        }
    }
}
