using System.Collections.Generic;
using System.Threading.Tasks;
using DefenderServices.Interfaces;
using Infrastructure.Models;
using Infrastructure.Results;
using RepositoryService;
using RepositoryService.Interfaces;

namespace DefenderServices
{
    public class MenuBuilderService : IMenuBuilderService
    {
        private IJSONRepository<MenuItem> _menuItemRepository;

        public MenuBuilderService()
        {
            _menuItemRepository = RepositoryFactory.CreateRepository<MenuItem>();
        }

        #region CRUD
        public async Task<IResultWithData<IList<MenuItem>>> GetAvailableMunuItems()
        {
            var menuItemsResult = await _menuItemRepository.GetItemsAsync();

            return menuItemsResult;
        }

        public async Task<IResultWithData<MenuItem>> GetMunuItemById(int id)
        {
            var menuItemResult = await _menuItemRepository.GetItemAsync(id);

            return menuItemResult;
        }

        public async Task<IResult> AddMunuItems(MenuItem newMenuItem)
        {
            var result = await _menuItemRepository.AddItemAsync(newMenuItem);
            return result;
        }

        public async Task<IResult> UpdateMunuItems(MenuItem newMenuItem)
        {
            var result = await _menuItemRepository.UpdateItemAsync(newMenuItem);
            return result;
        }

        public async Task<IResult> RemoveMunuItems(int id)
        {
            var result = await _menuItemRepository.RemoveItemAsync(id);
            return result;
        }
        #endregion
    }
}
