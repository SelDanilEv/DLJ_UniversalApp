using Infrastructure.Models;
using Infrastructure.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Interfaces
{
    public interface IDefenderRepository
    {
        Task<IResultWithData<IList<MenuItem>>> GetMenuItemsAsync();
        Task<IResult> AddMenuItemsAsync(MenuItem newMenuItem);
        Task<IResult> UpdateMenuItemsAsync(MenuItem newMenuItem);
        Task<IResult> RemoveMenuItemsAsync(int id);
    }
}
