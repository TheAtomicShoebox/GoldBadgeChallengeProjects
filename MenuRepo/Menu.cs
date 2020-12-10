using System.Collections.Generic;

namespace Menu
{
    public class Menu
    {
        public int ItemNumber { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public decimal Price { get; set; }

        public Menu()
        {
            Ingredients = new List<string>();
        }

        public Menu(int num, string name, string desc, List<string> ingredients, decimal price)
        {
            ItemNumber = num;
            MealName = name;
            Description = desc;
            Ingredients = ingredients;
            Price = price;
        }
    }
}
