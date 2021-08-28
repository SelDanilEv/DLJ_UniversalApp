using DefenderServices;
using DefenderServices.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestsApp.Tests
{
    public class MenuItemMongoTests
    {

        [Fact, TestPriority(5)]
        public async Task CreateNewMenuItemTest()
        {
            var mongoOptions = TestConfiguration.GetMongoOption();

            IMenuBuilderService mock = new MenuBuilderService(mongoOptions);

            var countBefore = (await mock.GetAvailableMenuItems()).GetData.Count;

            var menuItem = new MenuItem()
            {
                Name = "test name",
                Link = new ActionLink()
                {
                    Area = "area",
                    Controller = "controller",
                    Action = "Action"
                },
                Priority = 1,
                IsBeta = false
            };

            await mock.AddMenuItems(menuItem);

            var countAfter = (await mock.GetAvailableMenuItems()).GetData.Count;

            Assert.True(countBefore + 1 == countAfter);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateMenuItemTest()
        {
            var mongoOptions = TestConfiguration.GetMongoOption();

            IMenuBuilderService mock = new MenuBuilderService(mongoOptions);

            var menuItem = new MenuItem()
            {
                Name = "test name",
                Link = new ActionLink()
                {
                    Area = "area",
                    Controller = "controller",
                    Action = "Action"
                },
                Priority = 1,
                IsBeta = false
            };

            await mock.AddMenuItems(menuItem);

            var menuItemsList = (await mock.GetAvailableMenuItems()).GetData;

            var countBefore = menuItemsList.Count;
            var ID = menuItemsList[menuItemsList.Count-1].Id;

            var menuItemOld = (await mock.GetMenuItemById(ID)).GetData;

            var updatedMenuItem = new MenuItem()
            {
                Id = ID,
                Name = menuItemOld.Name + "2",
                IsBeta = !menuItemOld.IsBeta,
                Priority = menuItemOld.Priority + 5
            };

            await mock.UpdateMenuItems(updatedMenuItem);

            var menuItemNew = (await mock.GetMenuItemById(ID)).GetData;

            var countAfter = (await mock.GetAvailableMenuItems()).GetData.Count;

            Assert.True(countBefore == countAfter);
            Assert.True(menuItemOld.IsBeta != menuItemNew.IsBeta);
            Assert.True(menuItemOld.Name != menuItemNew.Name + "2");
            Assert.True(menuItemOld.Priority != menuItemNew.Priority + 5);
        }

        [Fact, TestPriority(0)]
        public async Task RemoveMenuItemTest()
        {
            var mongoOptions = TestConfiguration.GetMongoOption();

            IMenuBuilderService mock = new MenuBuilderService(mongoOptions);

            var menuItem = new MenuItem()
            {
                Name = "test name",
                Link = new ActionLink()
                {
                    Area = "area",
                    Controller = "controller",
                    Action = "Action"
                },
                Priority = 1,
                IsBeta = false
            };

            await mock.AddMenuItems(menuItem);

            var menuItemsList = (await mock.GetAvailableMenuItems()).GetData;

            var countBefore = menuItemsList.Count;
            var ID = menuItemsList[menuItemsList.Count - 1].Id;

            await mock.RemoveMenuItems(ID);

            var countAfter = (await mock.GetAvailableMenuItems()).GetData.Count;

            var menuItemFromRepository = (await mock.GetMenuItemById(ID)).GetData;

            Assert.True(countBefore - 1 == countAfter);
            Assert.Null(menuItemFromRepository);
        }
    }
}
