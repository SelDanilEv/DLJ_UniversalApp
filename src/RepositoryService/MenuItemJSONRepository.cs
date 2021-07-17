using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

using Infrastructure.Results;
using Infrastructure.Models;
using System.Linq;

namespace RepositoryService
{
    public class MenuItemJSONRepository : BaseJSONRepository<MenuItem>
    {
        public override async Task<IResultWithData<IList<MenuItem>>> GetItemsAsync()
        {
            var result = new Result<IList<MenuItem>>();

            try
            {
                var jsonString = await File.ReadAllTextAsync(_menuItemRepositoryPath);
                var menuItems = JsonSerializer.Deserialize<List<MenuItem>>(jsonString);
                result.Data = SortMenuItemsByPriority(menuItems);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<MenuItem>> GetItemAsync(int id)
        {
            var menuItemsResult = await GetItemsAsync();
            var result = new Result<MenuItem>();

            if (!menuItemsResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read menu items";
                return result;
            }

            result.Data = menuItemsResult.GetData.FirstOrDefault(x => x.ID == id);

            return result;
        }

        public override async Task<IResult> AddItemAsync(MenuItem newMenuItem)
        {
            var menuItemsResult = await GetItemsAsync();
            var result = new Result();

            if (!menuItemsResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read menu items";
                return result;
            }

            try
            {
                var menuItems = menuItemsResult.GetData;
                newMenuItem.ID = FindMinimumFreeId(menuItems);
                menuItems.Add(newMenuItem);
                menuItems = SortById(menuItems);
                var jsonString = JsonSerializer.Serialize(menuItems);
                File.WriteAllText(_menuItemRepositoryPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> UpdateItemAsync(MenuItem updatedMenuItem)
        {
            var menuItemsResult = await GetItemsAsync();
            var result = new Result();

            if (!menuItemsResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read menu items";
                return result;
            }

            try
            {
                var menuItems = menuItemsResult.GetData;
                var itemToUpdate = menuItems.First(x => x.ID == updatedMenuItem.ID);
                itemToUpdate.Name = updatedMenuItem.Name;
                itemToUpdate.Link = updatedMenuItem.Link;
                itemToUpdate.IsBeta = updatedMenuItem.IsBeta;
                itemToUpdate.Priority = updatedMenuItem.Priority;
                var jsonString = JsonSerializer.Serialize(menuItems);
                File.WriteAllText(_menuItemRepositoryPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> RemoveItemAsync (int id)
        {
            var menuItemsResult = await GetItemsAsync();
            var result = new Result();

            if (!menuItemsResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read menu items";
                return result;
            }

            try
            {
                var menuItems = menuItemsResult.GetData;
                var itemToUpdate = menuItems.Remove(menuItems.First(x => x.ID == id));
                var jsonString = JsonSerializer.Serialize(menuItems);
                File.WriteAllText(_menuItemRepositoryPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }


        private IList<MenuItem> SortMenuItemsByPriority(IList<MenuItem> unsortedList)
        {
            var sortedList = unsortedList.OrderBy(x => -x.Priority);
            return sortedList.ToList();
        }
    }
}
