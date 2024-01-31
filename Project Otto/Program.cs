//==========================================================
// Student Number : S10258408
// Student Name : Noel Ng
// Partner Name : -
//==========================================================

using Project_Otto;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;

List<Customer> customerList = new List<Customer>();
List<Order> goldOrderList = new List<Order>();
List<Order> regularOrderList = new List<Order>();
Queue<Order> goldOrderQueue = new Queue<Order>();
Queue<Order> regularOrderQueue = new Queue<Order>();
List<Customer> birthdayRedeemed = new List<Customer>();

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
    Console.WriteLine("-------ADVANCED-------");
    Console.WriteLine("[7] Process & Checkout");
    Console.WriteLine("----------------------");
}

static void Option6Menu()
{
    string[] options =
    {
        "Choose an existing icecream to modify",
        "Add a new icecream",
        "Delete an icecream from the order",
    };

    Console.WriteLine("---------MENU---------");
    int n = 0;
    foreach (string i in options)
    {
        Console.WriteLine($"[{n += 1}] {i}");
    }
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
static void InitOrders(List<Customer> customerList, List<Order> goldOrderList, List<Order> regularOrderList)
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

        if (goldOrderList.Count > 0) 
        {
            
            foreach (Order o in goldOrderList) /* Checking for existing order */
            {
                if (o.Id == id)
                {
                    o.IceCreamList.Add(iceCream);
                    orderExists = true;
                    break;
                }
            }

            if (!orderExists)
            {
                if (regularOrderList.Count > 0)
                {
                    foreach (Order o in regularOrderList)
                    {
                        if (o.Id == id)
                        {
                            o.IceCreamList.Add(iceCream);
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
            newOrder.IceCreamList.Add(iceCream);

            foreach (Customer customer in customerList)
            {
                if (customer.MemberId == memberId)
                {
                    if (customer.Rewards.Tier == "Gold")
                    {
                        goldOrderList.Add(newOrder);
                    }
                    else
                    {
                        regularOrderList.Add(newOrder);
                    }
                    customer.OrderHistory.Add(newOrder);
                    break;
                }
            }
        }


    }
}

static void OptionSelector(List<Customer> customerList, Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue, List<Order> goldOrderList, List<Order> regularOrderList, List<Customer> birthdayRedeemed)
{
    while (true)
    {
        Console.Write("Enter your option: ");
        int option = 0;

        if (int.TryParse(Console.ReadLine(), out option))
        {

            if (option == 0)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            else if (option == 1)
            {
                Console.WriteLine("Chosen Option: [1] List all customers\n");
                ListCustomers(customerList);
            }

            else if (option == 2)
            {
                Console.WriteLine("Chosen Option: [2] List all current orders\n");
                ListCurrentOrders(goldOrderQueue, regularOrderQueue);
            }

            else if (option == 4)
            {
                Console.WriteLine("Chosen Option: [4] Create a customer’s order\n");
                CreateOrder(customerList, goldOrderQueue, regularOrderQueue, goldOrderList, regularOrderList);
            }

            else if (option == 6)
            {
                ModifyOrder(customerList);
            }

            else if (option == 7)
            {
                Console.WriteLine("Chosen Option: [7] Process & Checkout");
                ProcessOrder(customerList, goldOrderQueue, regularOrderQueue, goldOrderList, regularOrderList, birthdayRedeemed);
            }

            else
            {
                Console.WriteLine("Option does not exist.");
            }
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

        var fulfilled = (order.TimeFulfilled == DateTime.MinValue) ? "-" : order.TimeFulfilled.ToString();

        Console.WriteLine
            (
            "|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|",
            order.Id,
            order.TimeReceived.ToString(),
            fulfilled,
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

static void ListCustomers(List<Customer> customerList, Customer c = null)
{

    if (c == null)
    {
        Console.WriteLine("┌----------------------------------------------------------------------------------------┐");
        Console.WriteLine("|{0,-10}|{1,-11}|{2,-12}|{3,-19}|{4,-19}|{5,-12}|", "Name", "Member ID", "DOB", "Membership Status", "Membership Points", "Punch Card");
        Console.WriteLine("------------------------------------------------------------------------------------------");
        foreach (Customer customer in customerList)
        {
            Console.WriteLine("|{0,-10}|{1,-11}|{2,-12}|{3,-19}|{4,-19}|{5,-12}|", customer.Name, customer.MemberId, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
        }
        Console.WriteLine("└----------------------------------------------------------------------------------------┘");
    }

    else
    {
        Console.WriteLine("┌----------------------------------------------------------------------------------------┐");
        Console.WriteLine("|{0,-10}|{1,-11}|{2,-12}|{3,-19}|{4,-19}|{5,-12}|", "Name", "Member ID", "DOB", "Membership Status", "Membership Points", "Punch Card");
        Console.WriteLine("------------------------------------------------------------------------------------------");
        Console.WriteLine("|{0,-10}|{1,-11}|{2,-12}|{3,-19}|{4,-19}|{5,-12}|", c.Name, c.MemberId, c.Dob.ToString("dd/MM/yyyy"), c.Rewards.Tier, c.Rewards.Points, c.Rewards.PunchCard);
        Console.WriteLine("└----------------------------------------------------------------------------------------┘");
    }
}

static void ListCurrentOrders(Queue<Order> goldOrderQueue,  Queue<Order> regularOrderQueue)
{
    Console.WriteLine("Gold Member Orders:");
    Console.WriteLine("┌--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┐");
    Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

    if (goldOrderQueue.Count > 0)
    {
        foreach (Order o in goldOrderQueue)
        {
            DisplayOrder(o);
        }
    }

    else
    {
        Console.WriteLine("|No Current Gold member orders.");
    }
    Console.WriteLine("└--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┘");

    Console.WriteLine("\nRegular Member Orders:");
    Console.WriteLine("┌--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┐");
    Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

    if (regularOrderQueue.Count > 0)
    {
        foreach (Order o in regularOrderQueue)
        {
            DisplayOrder(o);
        }
    }

    else
    {
        Console.WriteLine("|No Current Regular member orders.");
    }
    Console.WriteLine("└--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┘");
}

static void CreateOrder(List<Customer> customerList, Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue, List<Order> goldOrderList, List<Order> regularOrderList)
{
    ListCustomers(customerList);

    while (true)
    {
        int memId = 0;

        Console.Write("Enter a Customer's ID to retrieve Customer: ");
        try
        {
            memId = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Invalid Input! Member IDs are 6 Integers.");
            continue;
        }
        Customer customer = null;

        foreach (Customer c in customerList)
        {
            if (c.MemberId == memId)
            {
                customer = c;
                break;
            }
        }

        if (customer == null)
        {
            Console.WriteLine("Customer ID does not exist.");
        }

        else
        {
            if (customer.CurrentOrder != null)
            {
                Console.WriteLine("Existing order detected! Please use [4] to modify your current order, or use [7] to process and checkout your current order.");
                break;
            }
            Console.WriteLine($"Hi, {customer.Name}. Welcome to I.C. Treats, please fill up your order :)");
            Order newOrder = customer.MakeOrder();

            int highestOrderId = 0;
            foreach (Order o in goldOrderList) /* Finding next highest order ID that can be assigned */
            {
                if (o.Id >= highestOrderId)
                {
                    highestOrderId = o.Id;
                }
            }

            foreach (Order o in regularOrderList) /* Run second check on regular list */
            {
                if (o.Id >= highestOrderId)
                {
                    highestOrderId = o.Id;
                }
            }

            foreach (Order o in goldOrderQueue) /* Extra checks */
            {
                if (o.Id >= highestOrderId)
                {
                    highestOrderId = o.Id;
                }
            }

            foreach (Order o in regularOrderQueue) /* Extra Checks */
            {
                if (o.Id >= highestOrderId)
                {
                    highestOrderId = o.Id;
                }
            }


            newOrder.Id = highestOrderId+1;

            Console.WriteLine("Your Order Details:");
            Console.WriteLine("┌--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┐");
            Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            DisplayOrder(newOrder);
            Console.WriteLine("└--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┘");

            if (customer.Rewards.Tier == "Gold")
            {
                goldOrderQueue.Enqueue(newOrder);
                Console.WriteLine("Gold Member Order Completed.");
            } 

            else
            {
                regularOrderQueue.Enqueue(newOrder);
                Console.WriteLine("Regular Member Order Completed.");
            }
            break;
        }
    }
}

static void ModifyOrder(List<Customer> customerList)
{
    ListCustomers(customerList);
    bool outerLoopFlag = true;

    while (outerLoopFlag)
    {
        int memId = 0;

        Console.Write("Enter a Customer's ID to retrieve Customer: ");
        try
        {
            memId = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Invalid Input! Member IDs are 6 Integers.");
            continue;
        }
        Customer customer = null;

        foreach (Customer c in customerList)
        {
            if (c.MemberId == memId)
            {
                customer = c;
                break;
            }
        }

        if (customer == null)
        {
            Console.WriteLine("Customer ID does not exist.");
        }

        else
        {
            if (customer.CurrentOrder != null)
            {
                Console.WriteLine($"Hi, {customer.Name}. Welcome to I.C. Treats, here is your current order. :)");
                while (true)
                {
                    Console.WriteLine("Your Order Details:");
                    Console.WriteLine("┌--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┐");
                    Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    DisplayOrder(customer.CurrentOrder);
                    Console.WriteLine("└--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┘");
                    Option6Menu();
                    Console.Write("Enter your option: ");
                    int option = 0;
                    if (int.TryParse(Console.ReadLine(), out option) && option > 0 && option <= 3)
                    {
                        if (option == 1)
                        {
                            Console.WriteLine("Chosen Option: [1] Choose an existing icecream to modify\n");
                            Console.Write("Enter which icecream you want to modify (Starting from 1 as the first icecream): ");
                            int toBeModified = 0;
                            if (int.TryParse(Console.ReadLine(), out toBeModified) && toBeModified - 1 >= 0 && toBeModified - 1 < customer.CurrentOrder.IceCreamList.Count())
                            {
                                toBeModified -= 1;
                                customer.CurrentOrder.ModifyIceCream(toBeModified);
                            }

                            else
                            {
                                Console.WriteLine("Icecream of that position does not exist.");
                                continue;
                            }
                        }

                        else if (option == 2)
                        {
                            Console.WriteLine("Chosen Option: [2] Add a new icecream\n");
                            IceCream ic = null;
                            customer.CurrentOrder.AddIceCream(ic);
                        }

                        else if (option == 3)
                        {
                            Console.WriteLine("Chosen Option: [3] Delete an icecream from the order\n");
                            Console.Write("Enter which icecream you want to delete (Starting from 1 as the first icecream): ");
                            int toBeDeleted = 0;
                            if (int.TryParse(Console.ReadLine(), out toBeDeleted) && toBeDeleted - 1 >= 0 && toBeDeleted - 1 < customer.CurrentOrder.IceCreamList.Count())
                            {
                                toBeDeleted -= 1;
                                customer.CurrentOrder.DeleteIceCream(toBeDeleted);
                            }

                            else
                            {
                                Console.WriteLine("Icecream of that position does not exist.");
                                continue;
                            }
                        }

                        Console.WriteLine("Your New Order Details:");
                        Console.WriteLine("┌--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┐");
                        Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        DisplayOrder(customer.CurrentOrder);
                        Console.WriteLine("└--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┘");
                        outerLoopFlag = false;
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Option does not exist.");
                    }
                }
            }

            else
            {
                Console.WriteLine("You do not have a current order.");
                break;
            }
        }
    }
}

static void ProcessOrder(List<Customer> customerList, Queue<Order> goldOrderQueue, Queue<Order> regularOrderQueue, List<Order> goldOrderList, List<Order> regularOrderList, List<Customer> birthdayRedeemed)
{
    Order order = null;

    if (goldOrderQueue.Count > 0)
    {
        order = goldOrderQueue.Dequeue();
        goldOrderList.Add(order);
    }

    else if (regularOrderQueue.Count > 0)
    {
        order = regularOrderQueue.Dequeue();
        regularOrderList.Add(order);
    }

    else
    {
        Console.WriteLine("No orders in the gold and regular queues.");
    }

    Console.WriteLine("Order Details:");
    Console.WriteLine("┌--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┐");
    Console.WriteLine("|{0,-4}|{1,-23}|{2,-23}|{3,-8}|{4,-8}|{5,-7}|{6,-16}|{7,-12}|{8,-12}|{9,-12}|{10,-11}|{11,-11}|{12,-11}|{13,-11}|", "ID", "Time Received", "Time Fulfilled", "Option", "Scoops", "Dipped", "Waffle Flavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    DisplayOrder(order);
    Console.WriteLine("└--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------┘");
    double total = order.CalculateTotal();

    Customer customer = null;

    foreach (Customer c in customerList)
    {
        if (c.CurrentOrder == order)
        {
            customer = c;
        }
    }

    Console.WriteLine("Customer Details:");
    ListCustomers(customerList, customer);

    bool isBday = customer.IsBirthday();
    IceCream bdayIcecream = null; //marker to make sure the birthday icecream is not discounted by punchcard again

    if (isBday && !birthdayRedeemed.Contains(customer))
    {
        double mostExpensive = 0;
        foreach (IceCream ic in order.IceCreamList)
        {
            if (ic.CalculatePrice() > mostExpensive)
            {
                mostExpensive = ic.CalculatePrice();
                bdayIcecream = ic;
            }
        }
        Console.WriteLine($"Happy Birthday {customer.Name}! We will be gifting you the most expensive icecream (${mostExpensive}) in your order free of charge.");
        birthdayRedeemed.Add(customer);
        total -= mostExpensive;
    }

    if (customer.Rewards.PunchCard >= 10)
    {
        Console.WriteLine("Completed Punch Card Detected. First icecream in your order that has not been discounted will be free of charge.");
        IceCream toBeDiscounted = null;
        if (bdayIcecream != null)
        {
            if (bdayIcecream == order.IceCreamList[0])
            {
                if (order.IceCreamList.Count > 1)
                {
                    toBeDiscounted = order.IceCreamList[1];
                }
                else
                {
                    Console.WriteLine("Only one icecream in order, we will prioritising the birthday discount instead.");
                }
            }
            else
            {
                toBeDiscounted = order.IceCreamList[0];
            }
        }
        else
        {
            toBeDiscounted = order.IceCreamList[0];
        }

        if (bdayIcecream != toBeDiscounted && toBeDiscounted != null)
        {
            total -= toBeDiscounted.CalculatePrice();
            customer.Rewards.PunchCard = 0;
        }
    }

    if (total > 0)
    {
        if (customer.Rewards.Tier != "Ordinary" && customer.Rewards.Points > 0)
        {
            while (true)
            {
                int pointsToOffset = 0;
                Console.Write($"You have {customer.Rewards.Points} points in your Point Card. How many would you like to offset on this order (${total:0.00})? (0 if none, each point worth $0.02): ");
                if (int.TryParse(Console.ReadLine(), out pointsToOffset) && pointsToOffset <= customer.Rewards.Points)
                {
                    if (pointsToOffset == 0)
                    {
                        Console.WriteLine("Not redeeming any points.");
                    }
                    else if (pointsToOffset*0.02 > total)
                    {
                        Console.WriteLine($"You are redeeming too many points! The maximum number of points to redeem for this order is {Math.Floor(total/0.02)} points.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Offsetting {pointsToOffset} points from your card.");
                        customer.Rewards.Points -= pointsToOffset;
                        total -= (pointsToOffset * 0.02);
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please enter a valid number of points to redeem.");
                }
            }
        }
    }

    Console.WriteLine("┌----------┐");
    Console.WriteLine("|Final Bill|");
    Console.WriteLine("|----------|");
    Console.WriteLine("|----------|");
    Console.WriteLine("|----------|");
    Console.WriteLine("|----------|");
    Console.WriteLine("|----------|");
    Console.WriteLine("|----------|");
    Console.WriteLine($"|${total,9:0.00}|");
    Console.WriteLine("└----------┘");
    Console.WriteLine("Press any key to make payment.");
    Console.ReadKey();

    int punchcardIncrement = 0;
    foreach (IceCream ic in order.IceCreamList)
    {
        punchcardIncrement += 1;
    }

    if (punchcardIncrement+customer.Rewards.PunchCard > 10)
    {
        customer.Rewards.PunchCard = 10;
    }
    else
    {
        customer.Rewards.PunchCard += punchcardIncrement;
    }

    Console.WriteLine("Payment completed. Your Punch Card has been updated.");

    double pointsToAdd = Math.Floor(total * 0.72);
    customer.Rewards.Points += Convert.ToInt32(pointsToAdd);

    if (customer.Rewards.Points >= 100)
    {
        if (customer.Rewards.Tier == "Ordinary" || customer.Rewards.Tier == "Silver")
        {
            customer.Rewards.Tier = "Gold";
        }
    }

    else if (customer.Rewards.Points >= 50)
    {
        if (customer.Rewards.Tier == "Ordinary")
        {
            customer.Rewards.Tier = "Silver";
        }
    }

    order.TimeFulfilled = DateTime.Now;
    customer.OrderHistory.Add(order);
    customer.CurrentOrder = null;
}

InitCustomers(customerList);
InitOrders(customerList, goldOrderList, regularOrderList);
Menu();
OptionSelector(customerList, goldOrderQueue, regularOrderQueue, goldOrderList, regularOrderList, birthdayRedeemed);