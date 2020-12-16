using System.Collections.Generic;

namespace MenuItems
{
    public class MenuRepo
    {
        private List<Menu> menuItems = new List<Menu>();

        //C
        public void AddMenuItem(Menu menuItem)
        {
            menuItems.Add(menuItem);
        }

        //R
        public List<Menu> GetMenuItems()
        {
            return menuItems;
        }

        //U
        public bool UpdateExistingMenuItem(int number, Menu newMenuItem)
        {
            Menu oldItem = GetMenuItemByNumber(number);
            if(oldItem != null)
            {
                oldItem.MealName = newMenuItem.MealName;
                oldItem.Description = newMenuItem.Description;
                oldItem.ItemNumber = newMenuItem.ItemNumber;
                oldItem.Ingredients = newMenuItem.Ingredients;
                oldItem.Price = newMenuItem.Price;
                return true;
            }
            return false;
        }

        //D
        public bool RemoveMenuItem(int number)
        {
            Menu item = GetMenuItemByNumber(number);
            if(item != null)
            {
                int initialCount = menuItems.Count;
                menuItems.Remove(item);
                if(initialCount > menuItems.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Menu GetMenuItemByNumber(int number)
        {
            foreach(Menu item in menuItems)
            {
                if(item.ItemNumber == number)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
