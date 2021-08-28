using Infrastructure.Models;
using Infrastructure.Results;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Interfaces
{
    public interface IRepository<Model>
    {
        Task<IResultWithData<IList<Model>>> GetItemsAsync();
        Task<IResultWithData<IList<Model>>> GetItemsWithFilterAsync(FilterDefinition<Model> filter);
        Task<IResultWithData<Model>> GetItemWithFilterAsync(FilterDefinition<Model> filter);
        Task<IResultWithData<Model>> GetItemAsync(string id);
        Task<IResult> AddItemAsync(Model newMenuItem);
        Task<IResult> UpdateItemAsync(Model newMenuItem);
        Task<IResult> RemoveItemAsync(string id);
    }
}
