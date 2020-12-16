using System;
using System.Collections.Generic;
using MenuItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MenuRepo_Tests
{
    [TestClass]
    public class MenuRepoTests
    {
        [TestMethod]
        public void AddMenuItem_ShouldGetNotNull()
        {
            MenuRepo repo = new MenuRepo();
            List<string> ing = new List<string>(){ "ing1", "ing2", "ing3" };
            Menu menu = new Menu()
            {
                ItemNumber = 1,
                MealName = "Name",
                Ingredients = ing,
                Description = "desc",
                Price = 2.50m
            };
            repo.AddMenuItem(menu);
            Assert.IsNotNull(repo.GetMenuItems());
        }

        [TestMethod]
        public void UpdateMenuItem_ShouldUpdateItem()
        {
            MenuRepo repo = new MenuRepo();
            List<string> ing = new List<string>() { "ing1", "ing2", "ing3" };
            Menu menu = new Menu()
            {
                ItemNumber = 1,
                MealName = "Name",
                Ingredients = ing,
                Description = "desc",
                Price = 2.50m
            };
            List<string> ing2 = new List<string>() { "1ing", "2ing", "3ing" };
            Menu menu2 = new Menu()
            {
                ItemNumber = 2,
                MealName = "Name2",
                Ingredients = ing2,
                Description = "desc2",
                Price = 2.75m
            };
            repo.AddMenuItem(menu);
            repo.UpdateExistingMenuItem(menu.ItemNumber, menu2);
            Menu actual = repo.GetMenuItems()[0];
            Assert.AreEqual(menu2, actual);
        }

        [TestMethod]
        public void RemoveMenuItem_ShouldGetNull()
        {
            MenuRepo repo = new MenuRepo();
            List<string> ing = new List<string>() { "ing1", "ing2", "ing3" };
            Menu menu = new Menu()
            {
                ItemNumber = 1,
                MealName = "Name",
                Ingredients = ing,
                Description = "desc",
                Price = 2.50m
            };
            repo.AddMenuItem(menu);
            repo.RemoveMenuItem(menu.ItemNumber);
            Assert.IsNull(repo.GetMenuItems());
        }
    }
}
