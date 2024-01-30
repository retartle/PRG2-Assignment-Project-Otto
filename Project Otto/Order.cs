using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public void ModifyIceCream(int index)
        {
            IceCream ic = IceCreamList[index];
            Console.WriteLine("We will be modifying this icecream from the beginning. Please input your configuration. (Note that choosing a new option means creating a totally new icecream and deleting the old one)");
            AddIceCream(ic);
        }

        public void AddIceCream(IceCream ic)
        {
            string[] options = { "cup", "cone", "waffle" };
            string chosenOption = "";
            bool outerLoopFlag = true;
            bool modifyMode = false; // To change some parts of the code, example  not adding an new icecream object to the list as it is already in there, and also turning off the add new icecream loop
            bool differentOptionFromOriginal = false;

            if (ic != null)
            {
                Console.WriteLine("Modify");
                modifyMode = true;
            }

            while (outerLoopFlag)
            {
                while (true)
                {
                    Console.Write("Option (Cup/Cone/Waffle): ");
                    chosenOption = Console.ReadLine().ToLower().Trim();
                    if (options.Contains(chosenOption))
                    {
                        break;
                    }
                    Console.WriteLine("Invalid Option! Please input one of the specified options.");
                }

                if (chosenOption == "cup")
                {
                    if (!modifyMode)
                    {
                        ic = new Cup();
                    }
                    else if (ic is not Cup)
                    {
                        IceCreamList.Remove(ic);
                        ic = new Cup();
                        differentOptionFromOriginal = true;
                    }
                    ic.Option = "Cup";
                    ic.Flavours = new List<Flavour>();
                    ic.Toppings = new List<Topping>();
                    ConfigIceCream(ic);

                    if (!modifyMode)
                    {
                        IceCreamList.Add(ic);
                        TimeReceived = DateTime.Now;
                    }
                    else if (differentOptionFromOriginal)
                    {
                        IceCreamList.Add(ic);
                    }
                }
                else if (chosenOption == "cone")
                {
                    if (!modifyMode)
                    {
                        ic = new Cone();
                    }
                    else if (ic is not Cone)
                    {
                        IceCreamList.Remove(ic);
                        ic = new Cone();
                        differentOptionFromOriginal = true;
                    }
                    ic.Option = "Cone";
                    ic.Flavours = new List<Flavour>();
                    ic.Toppings = new List<Topping>();
                    ConfigIceCream(ic);
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
                    ((Cone)ic).Dipped = dipped;
                    if (!modifyMode)
                    {
                        IceCreamList.Add(ic);
                        TimeReceived = DateTime.Now;
                    }
                    else if (differentOptionFromOriginal)
                    {
                        IceCreamList.Add(ic);
                    }
                }
                else if (chosenOption == "waffle")
                {
                    if (!modifyMode)
                    {
                        ic = new Waffle();
                    }
                    else if (ic is not Waffle)
                    {
                        IceCreamList.Remove(ic);
                        ic = new Waffle();
                        differentOptionFromOriginal = true;
                    }
                    ic.Option = "Waffle";
                    ic.Flavours = new List<Flavour>();
                    ic.Toppings = new List<Topping>();
                    ConfigIceCream(ic);

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
                                    ((Waffle)ic).WaffleFlavour = option;
                                    break;
                                }

                                else
                                {
                                    Console.WriteLine("Invalid Input! Please input one of the specified waffle flavours.");
                                }
                            }
                            break;
                        }

                        else if (reply == "n" || reply == "no")
                        {
                            ((Waffle)ic).WaffleFlavour = "original";
                            Console.WriteLine("Original Waffle Flavour Configured.");
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Input! Please input between (Y/N).");
                        }
                    }

                    if (!modifyMode)
                    {
                        IceCreamList.Add(ic);
                        TimeReceived = DateTime.Now;
                    }
                    else if (differentOptionFromOriginal)
                    {
                        IceCreamList.Add(ic);
                    }
                }

                if (!modifyMode)
                {
                    while (true)
                    {
                        Console.Write("Would you like to add another ice cream? (Y/N):");
                        string input = Console.ReadLine().ToLower().Trim();

                        if (input == "y" || input == "yes")
                        {
                            Console.WriteLine("Configure your new ice cream.");
                            break;
                        }

                        else if (input == "n" || input == "no")
                        {
                            Console.WriteLine("Not adding another ice cream.");
                            outerLoopFlag = false;
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Input! Please input between (Y/N).");
                        }
                    }
                }

                else
                {
                    outerLoopFlag = false;
                }
            }
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
                    Console.WriteLine("Premium Flavours Chosen.");
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

                while (true)
                {
                    if (premium)
                    {
                        string[] flavours = { "durian", "ube", "sea salt" };
                        Console.Write($"Premium Flavour {i} (Durian/Ube/Sea Salt): ");
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
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Input! Please input one of the specified premium flavours.");
                        }
                    }

                    else
                    {
                        string[] flavours = { "vanilla", "chocolate", "strawberry" };
                        Console.Write($"Regular Flavour {i} (Vanilla/Chocolate/Strawberry): ");
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
                                Flavour newFlavour = new Flavour(option, false, 1);
                                ic.Flavours.Add(newFlavour);
                            }
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Input! Please input one of the specified flavours.");
                        }
                    }
                }
            }

            bool addToppings = false;
            int amt = 0;
            while (true)
            {
                Console.Write($"Any Toppings? (Y/N): "); /* Toppings */
                string reply = Console.ReadLine().ToLower().Trim();

                if (reply == "y" || reply == "yes")
                {
                    addToppings = true;
                    while (true)
                    {
                        Console.Write("How many? (1-4): ");
                        if (int.TryParse(Console.ReadLine(), out amt) && amt > 0 && amt <= 4)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input! Please input one of the numbers within the specified range.");
                        }
                    }
                    break;
                }

                else if (reply == "n" || reply == "no")
                {
                    Console.WriteLine("No Toppings added.");
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid Input! Please input between (Y/N).");
                }
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
                        i--;
                    }
                }
            }
        }

        public void DeleteIceCream(int index)
        {
            if (IceCreamList.Count() > 1)
            {
                IceCream ic = IceCreamList[index];
                Console.WriteLine($"Removing icecream {ic.Option}.");
                IceCreamList.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("You must have at least 1 icecream in the order. Delete Aborted.");
            }
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in IceCreamList)
            {
                double price = iceCream.CalculatePrice();
                total += price;
            }
            return total;
        }

        public override string ToString()
        {
            return $"ID: {Id}  Time Received: {TimeFulfilled}  Time Fulfilled: {TimeReceived}";
        }
    }
}
