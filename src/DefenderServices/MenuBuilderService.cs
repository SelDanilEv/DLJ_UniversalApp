using System.Collections.Generic;
using System.Threading.Tasks;
using DefenderServices.Interfaces;
using Infrastructure.Models;
using Infrastructure.Options;
using Infrastructure.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RepositoryService;
using RepositoryService.Interfaces;

namespace DefenderServices
{
    public class MenuBuilderService : IMenuBuilderService
    {
        private IRepository<MenuItem> _menuItemRepository;

        public MenuBuilderService(IOptions<MongoOptions> mongoOption)
        {
            _menuItemRepository = RepositoryFactory.CreateRepository<MenuItem>(mongoOption?.Value);
        }

        #region CRUD
        public async Task<IResultWithData<IList<MenuItem>>> GetAvailableMenuItems()
        {
            var menuItemsResult = await _menuItemRepository.GetItemsAsync();

            return menuItemsResult;
        }

        public async Task<IResultWithData<MenuItem>> GetMenuItemById(string id)
        {
            var menuItemResult = await _menuItemRepository.GetItemAsync(id);

            return menuItemResult;
        }

        public async Task<IResult> AddMenuItems(MenuItem newMenuItem)
        {
            var result = await _menuItemRepository.AddItemAsync(newMenuItem);
            return result;
        }

        public async Task<IResult> UpdateMenuItems(MenuItem newMenuItem)
        {
            var result = await _menuItemRepository.UpdateItemAsync(newMenuItem);
            return result;
        }

        public async Task<IResult> RemoveMenuItems(string id)
        {
            var result = await _menuItemRepository.RemoveItemAsync(id);
            return result;
        }
        #endregion
    }
}
