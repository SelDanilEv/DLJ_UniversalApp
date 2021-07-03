using System;
using System.Collections.Generic;
using System.Linq;
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
        private IDefenderRepository _defenderRepository = new DefenderJsonRepository();

        public async Task<IResultWithData<IList<MenuItem>>> GetAvailableMunuItems()
        {
            var menuItemsResult = await _defenderRepository.GetMenuItemsAsync();

            return menuItemsResult;
        }

        public async Task<IResultWithData<MenuItem>> GetMunuItemById(int id)
        {
            var menuItemsResult = await _defenderRepository.GetMenuItemsAsync();

            var result = new Result<MenuItem>();

            if (menuItemsResult.IsSuccess)
            {
                result.Data = menuItemsResult.GetData.FirstOrDefault(x=> x.ID == id);
            }
            else
            {
                result.Status = ResultStatus.Error;
            }

            return result;
        }

        public async Task<IResult> AddMunuItems(MenuItem newMenuItem)
        {
            var result = await _defenderRepository.AddMenuItemsAsync(newMenuItem);
            return result;
        }

        public async Task<IResult> UpdateMunuItems(MenuItem newMenuItem)
        {
            var result = await _defenderRepository.UpdateMenuItemsAsync(newMenuItem);
            return result;
        }

        public async Task<IResult> RemoveMunuItems(int id)
        {
            var result = await _defenderRepository.RemoveMenuItemsAsync(id);
            return result;
        }
    }
}
