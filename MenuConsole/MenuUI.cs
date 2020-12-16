using System;
using System.Collections.Generic;

namespace MenuItems
{
    public class MenuUI
    {
        private MenuRepo menuRepo = new MenuRepo();
        public void Run()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Select an Option:\n" +
                                  "1.\tDisplay Menu Items\n" +
                                  "2.\tCreate New Menu Item\n" +
                                  "3.\tUpdate Menu Items\n" +
                                  "4.\tDelete Menu Items\n" +
                                  "5.\tExit");
                char response = Console.ReadKey().KeyChar;
                switch (response)
                {
                    case '1':
                        DisplayMenuItems();
                        PressToContinue();
                        break;
                    case '2':
                        AddMenuItems();
                        break;
                    case '3':
                        UpdateMenuItems();
                        break;
                    case '4':
                        DeleteMenuItems();
                        break;
                    case '5':
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        PressToContinue();
                        break;
                }
            }
        }

        private bool AddMenuItems()
        {
            Console.Clear();
            Menu newItem = new Menu();
            Console.WriteLine("Menu Item Number: (q to quit)");
            newItem.ItemNumber = GetIntResponse("q", "Menu Item Number: (q to quit)");
            if (newItem.ItemNumber == 0)
            {
                return false;
            }
            Console.WriteLine("Meal Name: (q to quit)");
            newItem.MealName = GetResponse("q");
            if (newItem.MealName == null)
            {
                return false;
            }
            Console.WriteLine("Description: (q to quit)");
            newItem.Description = GetResponse("q");
            if (newItem.Description == null)
            {
                return false;
            }
            AddIngredients(newItem);
            Console.WriteLine("Price: (q to quit)");
            newItem.Price = GetDecimalResponse("q");
            if (newItem.Price == -1)
            {
                return false;
            }
            menuRepo.AddMenuItem(newItem);
            Console.WriteLine("Item Added.");
            PressToContinue();
            Console.WriteLine("Add another Item? (y/n)");
            if (GetYesNoResponse("y", "n"))
            {
                return AddMenuItems();
            }
            return true;
        }

        private bool UpdateMenuItems()
        {
            Menu item = UserSelectMenuItem();
            if(item != null)
            {
                Console.Clear();
                DisplayMenuItem(item);
                Menu newItem = new Menu();
                Console.WriteLine("New Menu Item Number: (q to quit)");
                newItem.ItemNumber = GetIntResponse("q", "Menu Item Number: (q to quit)");
                if (newItem.ItemNumber == -1)
                {
                    return false;
                }
                Console.WriteLine("New Meal Name: (q to quit)");
                newItem.MealName = GetResponse("q");
                if (newItem.MealName == null)
                {
                    return false;
                }
                Console.WriteLine("New Description: (q to quit)");
                newItem.Description = GetResponse("q");
                if (newItem.Description == null)
                {
                    return false;
                }
                AddIngredients(newItem);
                Console.WriteLine("New Price: (q to quit)");
                newItem.Price = GetDecimalResponse("q");
                if (newItem.Price == -1)
                {
                    return false;
                }
                DisplayMenuItem(newItem);
                Console.WriteLine("Is this correct?");
                if (GetYesNoResponse("y", "n"))
                {
                    menuRepo.UpdateExistingMenuItem(item.ItemNumber, newItem);
                    Console.WriteLine("Item Updated.");
                    PressToContinue();
                }
                else
                {
                    return UpdateMenuItems();
                }
                
                Console.WriteLine("Update another Item? (y/n)");
                if (GetYesNoResponse("y", "n"))
                {
                    return UpdateMenuItems();
                }
                return true;
            }
            return false;
        }

        private void DeleteMenuItems()
        {
            Menu item = UserSelectMenuItem();
            DisplayMenuItem(item);
            Console.WriteLine("Are you sure you want to remove this item?");
            if(GetYesNoResponse("y", "n"))
            {
                menuRepo.RemoveMenuItem(item.ItemNumber);
                Console.WriteLine("Item removed");
            }
        }

        private void DisplayMenuItems()
        {
            Console.WriteLine();
            List<Menu> items = menuRepo.GetMenuItems();
            foreach(Menu item in items)
            {
                DisplayMenuItem(item);
            }
        }

        private void DisplayMenuItem(Menu item)
        {
            Console.WriteLine($"{item.ItemNumber}.\t{item.MealName, -15}${item.Price, -8}\n{item.Description}");
            foreach(string ingredient in item.Ingredients)
            {
                Console.WriteLine($"{ingredient}");
            }
        }

        private void AddIngredients(Menu newItem)
        {
            Console.WriteLine("Add Ingredient: (q to quit, f to finish)");
            string response = GetResponse("q");
            if(response.ToLower() != "q")
            {
                if(response.ToLower() != "f")
                {
                    newItem.Ingredients.Add(response);
                    AddIngredients(newItem);
                }
            }
        }

        private Menu UserSelectMenuItem()
        {
            DisplayMenuItems();
            Console.WriteLine("Menu item id (q to quit):");
            int response = GetIntResponse("q");
            if(response != -1)
            {
                Menu item = menuRepo.GetMenuItemByNumber(response);
                if(item != null)
                {
                    return item;
                }
                Console.WriteLine("Invalid Response");
                PressToContinue();
                return UserSelectMenuItem();
            }
            return null;
        }

        private string GetResponse(string quitCharacter)
        {
            string response = Console.ReadLine();
            if(response.ToLower() == quitCharacter.ToLower())
            {
                return null;
            }
            return response;
        }

        private bool GetYesNoResponse(string affirmative, string negative)
        {
            string response = Console.ReadLine().ToLower();
            if(response == affirmative.ToLower())
            {
                return true;
            }
            if(response == negative.ToLower())
            {
                return false;
            }
            return false;
        }

        private int GetIntResponse(string quitCharacter, string prompt = "")
        {
            string response = Console.ReadLine();
            if (response.ToLower() == quitCharacter.ToLower())
            {
                return 0;
            }
            try
            {
                int intResponse = int.Parse(response);
                return intResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Response");
                PressToContinue();
                Console.WriteLine($"{prompt}");
                return GetIntResponse(quitCharacter);
            }
        }

        private decimal GetDecimalResponse(string quitCharacter)
        {
            string response = Console.ReadLine();
            if (response.ToLower() == quitCharacter.ToLower())
            {
                return 0;
            }
            try
            {
                decimal decimalResponse = decimal.Parse(response);
                return decimalResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Response");
                PressToContinue();
                GetDecimalResponse(quitCharacter);
            }
            return -1;
        }

        private void PressToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
