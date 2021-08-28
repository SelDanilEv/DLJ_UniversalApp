using Infrastructure.Models;
using Infrastructure.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefenderServices.Interfaces
{
    public interface IMenuBuilderService
    {
        Task<IResultWithData<IList<MenuItem>>> GetAvailableMenuItems();

        Task<IResultWithData<MenuItem>> GetMenuItemById(string id);

        Task<IResult> AddMenuItems(MenuItem newMenuItem);

        Task<IResult> UpdateMenuItems(MenuItem newMenuItem);

        Task<IResult> RemoveMenuItems(string id);
    }
}
