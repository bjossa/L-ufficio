using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
	public class Restaurant
	{
		private List<Waiter> waiters;
		private Chef chef;
		private Kitchen kitchen;
		private Dictionary<Waiter, int> tips;
		private int profit;
		public Restaurant(Kitchen kitchen, int numSeats)
		{
			this.kitchen = kitchen;
			tips = new Dictionary<Waiter, int>();
			profit = 0;
			waiters = new List<Waiter>();
		}

		public void hireWaiter(Waiter w, int salary)
		{
			waiters.Add(w);
			w.setSalary(salary);
			tips.Add(w, 0); // starts out with no tips
			w.Kitchen = kitchen;
		}

		public void fireWaiter(Waiter w)
		{
			if (waiters.Contains(w))
			{
				waiters.Remove(w);
			}
			else
			{
				throw new Exception("can't fire a waiter that doesn't work for you!");
			}
		}

		// Hire a chef. Can only have 1 chef at a time, so must not already have one
		public void hireChef(Chef c, int salary)
		{
			if (chef != null)
			{
				throw new Exception("This restaurant already has a chef!");
			}
			else
			{
				chef = c;
				c.setSalary(salary);
				kitchen.Chef = c;
			}
		}

		public void replaceChef(Chef c, int salary)
		{
			if (chef == null)
			{
				throw new Exception("can't replace a Chef when you don't have one!");
			}
			else
			{
				chef = c;
				c.setSalary(salary);
				kitchen.Chef = c;
			}
		}

		// pays the employees, prints out an error if cannot afford to pay employees
		public void payEmployees()
		{
			Console.WriteLine("pay day!");
			payWaiters();
			// then we pay the chef
			profit -= chef.receivePay();
			if (profit < 0)
			{
				Console.WriteLine("uh oh! Restaurant went bankrupt!!!");
			}


		}

		public void payWaiters()
		{
			foreach (Waiter w in waiters)
			{
				profit -= w.receivePay(tips[w]);
				tips[w] = 0; // just received his/her tips
			}
		}

		public void addRecipe(Recipe recipe)
		{
			kitchen.addRecipe(recipe);
		}
		// stores the tip that the customer left, and notes which waiter it goes to
		public void getTipFromCustomer(int amount, Waiter w)
		{
			if (tips.ContainsKey(w))
			{
				tips[w] = tips[w] + amount;
			} else
			{
				tips[w] = amount;
			}
		}

		// receieve the pay from the customer
		public void getPayFromCustomer(int amount)
		{
			profit += amount;
		}

		public int getProfit()
		{
			return profit;
		}

		// finds the waiter with the least number of customers, and adds this customer
		// to that waiter, then has the waiter take his order immediately
		public void addCustomer(Customer c)
		{
			Waiter w = waiters[0];
			int minCustomers = w.getCustomerCount();
			foreach (var waiter in waiters)
			{
				if (waiter.getCustomerCount() < minCustomers)
				{
					w = waiter;
					minCustomers = w.getCustomerCount();
				}
			}
			// now we have the waiter with the minimum number of customers
			w.addCustomer(c);
			w.takeOrder(c);
		}

		// receieve a new shipping of ingredients. Send them to the kitchen
		public void receieveShipping(Dictionary<string, int> s)
		{
			kitchen.receieveShipping(s);
		}
	}
}
