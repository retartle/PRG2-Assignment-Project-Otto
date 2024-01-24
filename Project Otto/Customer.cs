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
    public class Customer
    {
        private string name;
        private int memberId;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;

        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime Dob { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards { get; set; }

        public Customer() { }

        public Customer(string n, int id, DateTime dob)
        {
            Name = n;
            MemberId = id;
            Dob = dob;
            Rewards = new PointCard();
        }

        public void MakeOrder() //change void to order
        {
        }

        public bool IsBirthday()
        {
            return false;
        }

        public override string ToString()
        {
            return $"Name: {Name}  Member ID: {MemberId}  DOB: {Dob}";
        }
    }
}
