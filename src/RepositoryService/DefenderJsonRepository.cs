using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

using Infrastructure.Results;
using RepositoryService.Interfaces;
using Infrastructure.Models;
using System.Linq;
using System.Collections;

namespace RepositoryService
{
    public class DefenderJsonRepository : IDefenderRepository
    {
        private readonly string _menuItemPath = "TempDatabase/menuitems.json";

        public DefenderJsonRepository()
        {
        }

        #region menu items
        public async Task<IResultWithData<IList<MenuItem>>> GetMenuItemsAsync()
        {
            var menuItems = new List<MenuItem>();
            var result = new Result<IList<MenuItem>>(menuItems);

            try
            {
                var jsonString = File.ReadAllText(_menuItemPath);
                menuItems = JsonSerializer.Deserialize<List<MenuItem>>(jsonString);
                result.Data = SortByPriority(menuItems);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public async Task<IResult> AddMenuItemsAsync(MenuItem newMenuItem)
        {
            var menuItemsResult = await GetMenuItemsAsync();
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
                newMenuItem.ID = GetId(menuItems);
                menuItems.Add(newMenuItem);
                menuItems = SortById(menuItems);
                var jsonString = JsonSerializer.Serialize(menuItems);
                File.WriteAllText(_menuItemPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public async Task<IResult> UpdateMenuItemsAsync(MenuItem updatedMenuItem)
        {
            var menuItemsResult = await GetMenuItemsAsync();
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
                File.WriteAllText(_menuItemPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public async Task<IResult> RemoveMenuItemsAsync(int id)
        {
            var menuItemsResult = await GetMenuItemsAsync();
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
                var itemToUpdate = menuItems.Remove(menuItems.First(x=> x.ID == id));
                var jsonString = JsonSerializer.Serialize(menuItems);
                File.WriteAllText(_menuItemPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }
        #endregion

        private int GetId<T>(IList<T> models) where T : BaseModel
        {
            int newId = models.Count;

            int last = -1;
            for (int i = 0; i < models.Count; i++)
            {
                if (last + 1 != models[i].ID)
                {
                    return i;
                }
                last++;
            }

            return newId;
        }

        private IList<T> SortById<T>(IList<T> unsortedList) where T : BaseModel
        {
            IEnumerable<T> sortedList = unsortedList.OrderBy(x => x.ID);
            return sortedList.ToList();
        }

        private IList<T> SortByPriority<T>(IList<T> unsortedList) where T : MenuItem
        {
            IEnumerable<T> sortedList = unsortedList.OrderBy(x => -x.Priority);
            return sortedList.ToList();
        }
    }
}
