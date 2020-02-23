using System;
using System.Collections.Generic;

// namespace
namespace L_ufficio
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to L'ufficio!");
            Chef c = new Chef("Jordan");
            Kitchen kitchen = new Kitchen(c);
            Restaurant restaurant = new Restaurant(kitchen, 3);
            Customer cust1 = new Customer("Terry", new List<string> { "peanut" }, restaurant);
            Customer cust2 = new Customer("Bob", new List<string> { "peanut" }, restaurant);
            Recipe r = new Recipe(new List<string> { "tomato", "cheese" }, "salad", 100);
            kitchen.addRecipe(r);
            Waiter w = new Waiter("Michael");
            restaurant.hireWaiter(w, 15);
            restaurant.hireChef(c, 30);
            Dictionary<string, int> stock = new Dictionary<string, int>();
            stock["tomato"] = 1;
            stock["cheese"] = 1;
            stock["peanut"] = 2;
            restaurant.receieveShipping(stock);
            restaurant.addCustomer(cust1);
            restaurant.addCustomer(cust2);
            restaurant.payEmployees();
            Console.WriteLine("the restaurant made a total profit of " + restaurant.getProfit());
            Console.WriteLine("the waiter Michael earned a total salary of " + w.getTotalEarnings().ToString());
        }
    }
}
