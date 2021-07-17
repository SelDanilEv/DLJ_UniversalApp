using Infrastructure.Models;
using Infrastructure.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Interfaces
{
    public interface IJSONRepository<Model>
    {
        Task<IResultWithData<IList<Model>>> GetItemsAsync();
        Task<IResultWithData<Model>> GetItemAsync(int id);
        Task<IResult> AddItemAsync(Model newMenuItem);
        Task<IResult> UpdateItemAsync(Model newMenuItem);
        Task<IResult> RemoveItemAsync(int id);
    }
}
