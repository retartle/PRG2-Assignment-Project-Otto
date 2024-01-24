//==========================================================
// Student Number : S10258408
// Student Name : Noel Ng
// Partner Name : -
//==========================================================

using Project_Otto;
using System.Runtime.Intrinsics.Arm;

List<Customer> customerList = new List<Customer>();
Queue<Order> goldOrderQueue = new Queue<Order>();
Queue<Order> regularOrderQueue = new Queue<Order>();

static void Menu()
{
    string[] options =
    {
        "List all customers",
        "List all current orders",
        "Register a new customer [Disabled]",
        "Create a customer’s order",
        "Display order details of a customer [Disabled]",
        "Modify order details", 
    };

    Console.WriteLine("---------MENU---------");
    int n = 0;
    foreach (string i in options)
    {
        Console.WriteLine($"[{n+=1}] {i}");
    }
    Console.WriteLine("[0] Exit");
    Console.WriteLine("----------------------");
}

static void InitCustomers(List<Customer> customerList)
{
    string[] lines = File.ReadAllLines("C:\\Users\\neolt\\OneDrive - Ngee Ann Polytechnic\\CSFNP\\Y1S2\\PRG2\\Project\\Project Otto\\Project Otto\\customers.csv");

    for (int i = 1; i < lines.Length; i++)
    {
        string[] values = lines[i].Split(",");

        string name = values[0];
        int memberId = Convert.ToInt32(values[1]);
        DateTime Dob = DateTime.Parse(values[2]);
        string tier = values[3];
        int points = Convert.ToInt32(values[4]);
        int punchcard = Convert.ToInt32(values[5]);

        Customer temp = new Customer(name, memberId, Dob);
        temp.Rewards.Tier = tier;
        temp.Rewards.Points = points;
        temp.Rewards.PunchCard = punchcard;

        customerList.Add(temp);
    }
}

static void InitOrders(List<Customer> customerList, Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue)
{
    string[] lines = File.ReadAllLines("C:\\Users\\neolt\\OneDrive - Ngee Ann Polytechnic\\CSFNP\\Y1S2\\PRG2\\Project\\Project Otto\\Project Otto\\orders.csv");

    static void CreateFlavor(string flavour, List<Flavour> flavourList)
    {
        if (string.IsNullOrEmpty(flavour))
        {
            return;
        }

        bool checkPremium(string flavour)
        {
            if (flavour == "Vanilla" || flavour == "Chocolate" || flavour == "Strawberry")
            {
                return false;
            }

            else if (flavour == "Durian" || flavour == "Ube" || flavour == "Sea Salt")
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        Flavour temp = new Flavour(flavour, checkPremium(flavour), 1);

        flavourList.Add(temp);
    }

    static void CreateTopping(string topping, List<Topping> toppingList)
    {
        if (string.IsNullOrEmpty(topping))
        {
            return;
        }

        Topping temp = new Topping(topping);
        toppingList.Add(temp);
    }

    for (int i = 1; i < lines.Length; i++)
    {
        List<Flavour> flavourList = new List<Flavour>();
        List<Topping> toppingList = new List<Topping>();

        string[] values = lines[i].Split(",");

        int id = Convert.ToInt32(values[0]);
        int memberId = Convert.ToInt32(values[1]);
        DateTime timeReceived = DateTime.Parse(values[2]);
        DateTime timeFulfilled = DateTime.Parse(values[3]);
        string option = values[4];
        int scoops = Convert.ToInt32(values[5]);
        bool dipped = values[6] == "TRUE";
        string waffleFlavour = string.IsNullOrEmpty(values[7]) ? null : values[7];

        CreateFlavor(values[8], flavourList);
        CreateFlavor(values[9], flavourList);
        CreateFlavor(values[10], flavourList);
        CreateTopping(values[11], toppingList);
        CreateTopping(values[12], toppingList);
        CreateTopping(values[13], toppingList);
        CreateTopping(values[14], toppingList);

        IceCream iceCream = null; 

        if (option == "Cup")
        {
            iceCream = new Cup(option, scoops, flavourList, toppingList);
        }
        else if (option == "Cone")
        {
            iceCream = new Cone(option, scoops, flavourList, toppingList, dipped);
        }
        else if (option == "Waffle")
        {
            iceCream = new Waffle(option, scoops, flavourList, toppingList, waffleFlavour);
        }

        bool orderExists = false;

        if (goldOrderQueue.Count > 0) 
        {
            
            foreach (Order o in goldOrderQueue) /* Checking for existing order */
            {
                if (o.Id == id)
                {
                    o.AddIceCream(iceCream);
                    orderExists = true;
                    break;
                }
            }

            if (!orderExists)
            {
                if (regularOrderQueue.Count > 0)
                {
                    foreach (Order o in regularOrderQueue)
                    {
                        if (o.Id == id)
                        {
                            o.AddIceCream(iceCream);
                            orderExists = true;
                            break;
                        }
                    }
                }
            }
        }

        if (!orderExists)
        {
            Order newOrder = new Order { Id = id, TimeReceived = timeReceived, TimeFulfilled = timeFulfilled };
            newOrder.AddIceCream(iceCream);

            foreach (Customer customer in customerList)
            {
                if (customer.MemberId == memberId)
                {
                    if (customer.Rewards.Tier == "Gold")
                    {
                        goldOrderQueue.Enqueue(newOrder);
                    }
                    else
                    {
                        regularOrderQueue.Enqueue(newOrder);
                    }
                    customer.OrderHistory.Add(newOrder);
                    break;
                }
            }
        }


    }
}

static void OptionSelector(List<Customer> customerList, Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue)
{
    while (true)
    {
        Console.Write("Enter your option: ");
        int option = Convert.ToInt32(Console.ReadLine());

        if (option == 0)
        {
            Console.WriteLine("Exiting...");
            break;
        }

        else if (option == 1)
        {
            Console.WriteLine("Chosen Option: [1] List all customers\n");
            Option1(customerList);
        }

        else if (option == 2)
        {
            Console.WriteLine("Chosen Option: [2] List all current orders\n");
            Option2(goldOrderQueue, regularOrderQueue);
        }

        else if (option == 4)
        {
            break;
        }

        else if (option == 6)
        {
            break;
        }
        
        else
        {
            Console.WriteLine("Option does not exist.");
        }

        Console.WriteLine();
        Menu();
    }
}
static void DisplayOrder(Order order)
{
    foreach (IceCream ic in order.IceCreamList)
    {
        string dipped = "-";
        string waffle = "-";
        string f1 = "-";
        string f2 = "-";
        string f3 = "-";
        string t1 = "-";
        string t2 = "-";
        string t3 = "-";
        string t4 = "-";

        if (ic is Cone cone)
        {
            dipped = cone.Dipped ? "True" : "False";
        }

        if (ic is Waffle w)
        {
            waffle = w.WaffleFlavour;
        }

        if (ic.Flavours.Count >= 1)
        {
            f1 = ic.Flavours[0].Type;
        }
        if (ic.Flavours.Count >= 2)
        {
            f2 = ic.Flavours[1].Type;
        }
        if (ic.Flavours.Count == 3)
        {
            f3 = ic.Flavours[2].Type;
        }

        if (ic.Toppings.Count >= 1)
        {
            t1 = ic.Toppings[0].Type;
        }
        if (ic.Toppings.Count >= 2)
        {
            t2 = ic.Toppings[1].Type;
        }
        if (ic.Toppings.Count >= 3)
        {
            t3 = ic.Toppings[2].Type;
        }
        if (ic.Toppings.Count == 4)
        {
            t4 = ic.Toppings[3].Type;
        }

        Console.WriteLine
            (
            "|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|",
            order.Id,
            order.TimeReceived,
            order.TimeFulfilled,
            ic.Option,
            ic.Scoops,
            dipped,
            waffle,
            f1,
            f2,
            f3,
            t1,
            t2,
            t3,
            t4
            );
    }
}

static void Option1(List<Customer> customerList)
{
    Console.WriteLine("{0,-10}{1,-11}{2,-12}{3,-19}{4,-19}{5,-12}", "Name", "Member ID", "DOB", "Membership Status", "Membership Points", "Punch Card");
    foreach (Customer customer in customerList)
    {
        Console.WriteLine("{0,-10}{1,-11}{2,-12}{3,-19}{4,-19}{5,-12}", customer.Name, customer.MemberId, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
    }
}

static void Option2(Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue)
{
    Console.WriteLine("Gold Member Orders:");
    Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    foreach (Order o in goldOrderQueue)
    {
        DisplayOrder(o);
    }

    Console.WriteLine("\nRegular Member Orders:");
    Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    foreach (Order o in regularOrderQueue)
    {
        DisplayOrder(o);
    }
}

InitCustomers(customerList);
InitOrders(customerList, goldOrderQueue, regularOrderQueue);
Menu();
OptionSelector(customerList, goldOrderQueue, regularOrderQueue);
