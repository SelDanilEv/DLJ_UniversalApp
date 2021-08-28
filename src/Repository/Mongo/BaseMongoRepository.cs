using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Infrastructure.Results;
using RepositoryService.Interfaces;
using Infrastructure.Models;
using System.Linq;
using MongoDB.Driver;
using Infrastructure.Options;

namespace RepositoryService.Mongo
{
    public abstract class BaseMongoRepository<Model> : IRepository<Model> where Model : BaseModel
    {
        protected IMongoDatabase database;

        public BaseMongoRepository(MongoOptions mongoOption)
        {
            var connection = new MongoUrlBuilder(mongoOption.ConnectionString);
            MongoClient client = new MongoClient(mongoOption.ConnectionString);

            database = client.GetDatabase(connection.DatabaseName);
        }

        public virtual Task<IResult> AddItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<Model>> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<IList<Model>>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<IList<Model>>> GetItemsWithFilterAsync(FilterDefinition<Model> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<Model>> GetItemWithFilterAsync(FilterDefinition<Model> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResult> RemoveItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResult> UpdateItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        protected IList<Model> SortById(IList<Model> unsortedList)
        {
            var sortedList = unsortedList.OrderBy(x => x.Id);
            return sortedList.ToList();
        }
    }

    public class BaseJSONRepository : BaseMongoRepository<BaseModel>
    {
        public BaseJSONRepository(MongoOptions mongoOption) : base (mongoOption)
        {
        }
    }
}
