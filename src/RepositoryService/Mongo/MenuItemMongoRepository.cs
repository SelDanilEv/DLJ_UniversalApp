using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Infrastructure.Results;
using Infrastructure.Models;
using Infrastructure.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Infrastructure.Enums;

namespace RepositoryService.Mongo
{
    public class MenuItemMongoRepository : CommonMongoRepository<MenuItem>
    {

        public MenuItemMongoRepository(MongoOptions mongoOption) : base(mongoOption, MongoCollection.MenuItem) { }

        public override async Task<IResultWithData<IList<MenuItem>>> GetItemsAsync()
        {
            var result = new Result<IList<MenuItem>>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(new BsonDocument()).ToListAsync();
                result.Data = SortMenuItemsByPriority(unsortedResult);
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
