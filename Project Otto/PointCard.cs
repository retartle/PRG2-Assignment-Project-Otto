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
    public class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;

        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        public PointCard() { }

        PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        public void RedeemPoints(int points)
        {
            if (Points >= points)
            {
                Points -= points;
            }

            else
            {
                Console.WriteLine("Insufficient Points");
            }
        }

        public void Punch()
        {
            punchCard += 1;
        }

        public override string ToString()
        {
            return $"Points: {Points}  Punch Card: {PunchCard}  Tier: {Tier}";
        }
    }
}
