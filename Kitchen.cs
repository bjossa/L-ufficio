using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
	public class Kitchen : Subject
	{
		private Chef chef;
		public Chef Chef
		{
			get { return chef; }
			set { chef = value; }
		}
		private Dictionary<string, int> ingredients; // whenever this changes, call notifyObservers
		private List<Recipe> menu;
		public Kitchen(Chef c): base()
		{
			chef = c;
			ingredients = new Dictionary<string, int>();
			menu = new List<Recipe>();
		}

		public override void notifyObservers()
		{
			// first, we turn the ingredients into a list of ingredients which are currently
			// in stock
			List<string> stock = new List<string>();
			foreach (var ingredient in ingredients.Keys)
			{
				stock.Add(ingredient);
			}
			// then, we update the recipes with this stock
			foreach (var recipe in menu) {
				recipe.update(stock);
			}
		}

		public List<Recipe> getMenu()
		{
			return new List<Recipe>(menu); //copy it so callers can't change the menu
		}

		public void makeRecipe(Recipe r)
		{
			List<string> usedIngredients = r.prepare();
			// now, we need to update the stock of the kitchen
			foreach (var item in usedIngredients)
			{
				ingredients[item] = ingredients[item] - 1;
				if (ingredients[item] == 0)
				{
					ingredients.Remove(item);
				}
			}
			notifyObservers();
			
		}

		public void addRecipe(Recipe r)
		{
			menu.Add(r);
			base.addObserver(r);
		}

		public void removeRecipe(Recipe r)
		{
			if (menu.Contains(r))
			{
				menu.Remove(r);
				base.removeObserver(r);
			}
		}

		// adds the stock of all received ingredients to the ingredients of this kitchen
		public void receieveShipping(Dictionary<string, int> s)
		{
			// loop over ingredients in s, add the amount if already present, or create it otherwise
			foreach (var item in s.Keys)
			{
				if (ingredients.ContainsKey(item))
				{
					ingredients[item] = ingredients[item] + s[item];
				} else
				{
					ingredients[item] = s[item];
				}
			}
			notifyObservers();
		}
	}
}
