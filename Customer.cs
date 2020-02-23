using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
    public class Customer
    {
        public string name;
        private List<string> allergies;
        private Waiter waiter; // 1-1 relationship with customer
        private Restaurant r;


        public Customer(string name, List<string> allergies, Restaurant r)
        {
            this.allergies = allergies;
            this.name = name;
            this.r = r;
        }


        // checks all the recipes, finds the first he is not allergic to, orders it.
        // throws an exception if he is allergic to everything
        // also contains recipes that are not currently being served
        public Recipe makeOrder(List<Recipe> menu)
        {
            foreach (var recipe in menu)
            {
                if (canEat(recipe)) {
                    return recipe;
                }
            }
            // reach here if can't eat any recipes
            throw new Exception("allergic to everything");
        }

        // checks that the recipe does not contain any ingredients a customer is allergic to
        public bool canEat(Recipe r)
        {
            List<string> ingredients = r.getIngredientList();
            // loop over all the alergies, if any are contained in ingredients, return false
            foreach (var item in allergies)
            {
                if (ingredients.Contains(item))
                {
                    return false;
                }
            }
            return true; // not allergic to anything
        }
        
        // pays for the meal, and leave a tip
        public void pay(int amount)
        {
            r.getPayFromCustomer(amount);
            int tip = ((amount * 2) / 10); // a good customer that tips 20%
            r.getTipFromCustomer(tip, waiter);
        }

        public void setWaiter(Waiter w)
        {
            waiter = w;
            w.addCustomer(this);
        }
    }
}
