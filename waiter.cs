using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
    public class Waiter: RestaurantEmployee
    {
        private List<Customer> servingCustomers;
        private Kitchen kitchen;
        public Kitchen Kitchen
        { 
            get { return kitchen; }
            set { kitchen = value; }
        }

        // a waiter is not yet hired when it is created
        public Waiter(string name) : base(name)
        {
            servingCustomers = new List<Customer>();
        }

        public int receivePay(int tips)
        {
            int moneySpent = base.receivePay(); // money spent by restaurant to pay the waiter
            totalEarnings += tips;
            return moneySpent; // since the restaurant does not lose profit from tips
        }

        // takes an order from a customer. If it cannot be made, ask the customer to 
        // make another order. If it can be made, send the order to the kitchen
        // if there are no recipes that the customer can eat that are on the menu,
        // print out a string
        // Also charges the customer for the meal
        public void takeOrder(Customer c)
        {
            List<Recipe> menu = kitchen.getMenu(); // returns a copy
            bool continueLooping = true;
            while (continueLooping)
            {
                try
                {
                    if (menu.Count == 0)
                    {
                        throw new Exception("no items left to order on the menu");
                    }
                    Recipe recipe = c.makeOrder(menu);
                    if (recipe.onMenu)
                    {
                        kitchen.makeRecipe(recipe);
                        // now the customer needs to pay
                        c.pay(recipe.getPrice());
                        continueLooping = false; // stop if the customer successfully ordered
                    }
                    // if it was not on the menu, then delete it from the options, and ask the
                    // customer again
                    menu.Remove(recipe);
                }
                catch (Exception)
                {
                    Console.WriteLine("Customer is unable to order any food");
                    return;
                }
            }
        }

        public void addCustomer(Customer c)
        {
            if (!(servingCustomers.Contains(c)))
            {
                servingCustomers.Add(c);
                c.setWaiter(this);
            }
        }

        public int getCustomerCount()
        {
            return servingCustomers.Count;
        }

        


    }
}
