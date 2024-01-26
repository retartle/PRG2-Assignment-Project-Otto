using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
//==========================================================
// Student Number : S10258408
// Student Name : Noel Ng
// Partner Name : -
//==========================================================

using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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

        public Order MakeOrder()
        {
            CurrentOrder = new Order();
            string[] options = { "cup", "cone", "waffle" };
            string chosenOption = "";
            

            while (true)
            {
                Console.Write("Option (Cup/Cone/Waffle): ");
                chosenOption = Console.ReadLine().ToLower();
                if (options.Contains(chosenOption))
                {
                    break;
                }
                Console.WriteLine("Invalid Option! Please input one of the specified options.");
            }

            if (chosenOption == "cup")
            {
                Cup cup = new Cup();
                cup.Option = "Cup";
                cup.Flavours = new List<Flavour>();
                cup.Toppings = new List<Topping>();
                ConfigIceCream(cup);

                CurrentOrder.AddIceCream(cup);
                CurrentOrder.TimeReceived = DateTime.Now;
            }
            else if (chosenOption == "cone")
            {
                Cone cone = new Cone();
                cone.Option = "Cone";
                cone.Flavours = new List<Flavour>();
                cone.Toppings = new List<Topping>();
                ConfigIceCream(cone);
                bool dipped = false;
                while (true)
                {
                    Console.Write("Dipped (Y/N): ");
                    string reply = Console.ReadLine().ToLower().Trim();

                    if (reply == "y" || reply == "yes")
                    {
                        dipped = true;
                        break;
                    }

                    else if (reply == "n" || reply == "no")
                    {
                        Console.WriteLine("No Dipping Configured.");
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Invalid Input! Please input between (Y/N).");
                    }
                }

                cone.Dipped = dipped;
                CurrentOrder.AddIceCream(cone);
                CurrentOrder.TimeReceived = DateTime.Now;
            }
            else if (chosenOption == "waffle")
            {
                Waffle waffle = new Waffle();
                waffle.Option = "Waffle";
                waffle.Flavours = new List<Flavour>();
                waffle.Toppings = new List<Topping>();
                ConfigIceCream(waffle);

                while (true)
                {
                    Console.Write("Do you want a Waffle Flavour? (Y/N): ");
                    string waffleFlavour = "";
                    string reply = Console.ReadLine().ToLower().Trim();

                    if (reply == "y" || reply == "yes")
                    {
                        string[] flavours = { "red velvet", "charcoal", "pandan" };
                        while (true)
                        {
                            Console.Write($"Waffle Flavour (Red Velvet/Charcoal/Pandan): ");
                            string option = Console.ReadLine().ToLower().Trim();

                            if (flavours.Contains(option))
                            {
                                waffle.WaffleFlavour = option;
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Invalid Input! Please input one of the specified premium flavours.");
                            }
                        }
                        break;
                    }

                    else if (reply == "n" || reply == "no")
                    {
                        Console.WriteLine("Original Waffle Flavour Configured.");
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Invalid Input! Please input between (Y/N).");
                    }
                }

                CurrentOrder.AddIceCream(waffle);
                CurrentOrder.TimeReceived = DateTime.Now;
            }
            return CurrentOrder;
        }

        private void ConfigIceCream(IceCream ic)
        {
            int scoops = 0;
            while (true) /* Scoops */
            {
                Console.Write("Number of Scoops (1-3): ");
                if (int.TryParse(Console.ReadLine(), out scoops) && scoops > 0 && scoops <= 3)
                {
                    break;
                }
                Console.WriteLine("Invalid Input! Please input one of the specified options.");
            }
            ic.Scoops = scoops;

            static Flavour flavourExists(IceCream ic, string option)
            {
                return ic.Flavours.Find(flavour => flavour.Type == option); /* lambda expression of finding a matching object in the list */
            }

            for (int i = 1; i <= scoops; i++) /* Flavours */
            {
                Console.Write($"Premium Flavour for --> Flavour {i}? (Y/N): ");
                bool premium = false;
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "y" || input == "yes")
                {
                    premium = true;
                }

                else if (input == "n" || input == "no")
                {
                    Console.WriteLine("Regular Flavours Chosen.");
                }

                else
                {
                    Console.WriteLine("Invalid Input! Please input between (Y/N).");
                    i--;
                    continue;
                }

                if (premium)
                {
                    string[] flavours = { "durian", "ube", "sea salt" };
                    Console.Write($"Flavour {i} (Durian/Ube/Sea Salt): ");
                    string option = Console.ReadLine().ToLower().Trim();

                    if (flavours.Contains(option))
                    {
                        Flavour existingFlavour = flavourExists(ic, option);
                        if (existingFlavour != null)
                        {
                            existingFlavour.Quantity += 1;
                        }
                        else
                        {
                            Flavour newFlavour = new Flavour(option, true, 1);
                            ic.Flavours.Add(newFlavour);
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid Input! Please input one of the specified premium flavours.");
                        i--;
                    }
                }

                else
                {
                    string[] flavours = { "vanilla", "chocolate", "strawberry" };
                    Console.Write($"Flavour {i} (Vanilla/Chocolate/Strawberry): ");
                    string option = Console.ReadLine().ToLower().Trim();

                    if (flavours.Contains(option))
                    {
                        Flavour existingFlavour = flavourExists(ic, option);
                        if (existingFlavour != null)
                        {
                            existingFlavour.Quantity += 1;
                        }
                        else
                        {
                            Flavour newFlavour = new Flavour(option, true, 1);
                            ic.Flavours.Add(newFlavour);
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid Input! Please input one of the specified flavours.");
                        i--;
                    }
                }
            }

            Console.Write($"Any Toppings? (Y/N): "); /* Toppings */
            bool addToppings = false;
            int amt = 0;
            string reply = Console.ReadLine().ToLower().Trim();

            if (reply == "y" || reply == "yes")
            {
                addToppings = true;
                while (true)
                {
                    Console.Write("How many? (1-3): ");
                    if (int.TryParse(Console.ReadLine(), out amt) && amt > 0 && amt <= 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input! Please input one of the numbers within the specified range.");
                    }
                }
            }

            else if (reply == "n" || reply == "no")
            {
                Console.WriteLine("No Toppings added.");
            }

            else
            {
                Console.WriteLine("Invalid Input! Please input between (Y/N).");
            }

            if (addToppings)
            {
                for (int i = 1; i <= amt; i++) 
                {
                    string[] toppings = { "sprinkles", "mochi", "sago", "oreos" };
                    Console.Write($"Topping {i} (Sprinkles/Mochi/Sago/Oreos): ");
                    string option = Console.ReadLine().ToLower().Trim();

                    if (toppings.Contains(option))
                    {
                        Topping newTopping = new Topping(option);
                        ic.Toppings.Add(newTopping);
                    }

                    else
                    {
                        Console.WriteLine("Invalid Input! Please input one of the specified toppings.");
                    }
                }
            }
        }

        public bool IsBirthday()
        {
            return false; /* Placeholder, not implemented yet */
        }

        public override string ToString()
        {
            return $"Name: {Name}  Member ID: {MemberId}  DOB: {Dob}";
        }
    }
}
