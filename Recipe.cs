using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
   // this is an observer of kitchen, and when the stock of ingredients changes, this gets
   // updated and checks if it is still possible to be made
   // this can also be an interable of 'instruction'
    public class Recipe: Observer
    {
        private List<string> ingredients;
        public bool onMenu;
        private string name;
        private int price;

        public Recipe(List<string> ingredients, string name, int price)
        {
            this.ingredients = ingredients;
            onMenu = false; // a recipe starts off the menu
            this.name = name;
            this.price = price;
            
        }

        // updates with the stock of ingredients in the kitchen
        public override void update (List<string> stock)
        {
            checkAvailability(stock);
        }

        // checks that every ingredient is in stock. If not, take it off the menu
        // if so, put it on the menu
        public void checkAvailability(List<string> stock)
        {
            onMenu = true;
            foreach (var ingredient in ingredients)
            {
                if (!(stock.Contains(ingredient)))
                {
                    onMenu = false;
                    return; // no need to check the rest of the ingredients
                }

            }
        }

        public List<string> prepare()
        {
            Console.WriteLine("Yum! Cooking up " + name + "!");
            return ingredients;
        }

        public List<string> getIngredientList()
        {
            return ingredients;
        }

        public int getPrice()
        {
            return price;
        }
    }
}