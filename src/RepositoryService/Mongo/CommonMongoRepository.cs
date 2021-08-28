using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Infrastructure.Results;
using Infrastructure.Models;
using Infrastructure.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Infrastructure.Models.User;
using Infrastructure.Enums;

namespace RepositoryService.Mongo
{
    public class CommonMongoRepository<T> : BaseMongoRepository<T> where T : BaseModel
    {
        protected IMongoCollection<T> _mongoCollection;

        public CommonMongoRepository(MongoOptions mongoOption, MongoCollection collection): base(mongoOption)
        {
            _mongoCollection = database.GetCollection<T>(mongoOption[collection]);
        }

        public override async Task<IResultWithData<IList<T>>> GetItemsAsync()
        {
            var result = new Result<IList<T>>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(new BsonDocument()).ToListAsync();
                result.Data = unsortedResult;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<IList<T>>> GetItemsWithFilterAsync(FilterDefinition<T> filter)
        {
            var result = new Result<IList<T>>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(filter).ToListAsync();
                result.Data = unsortedResult;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<T>> GetItemWithFilterAsync(FilterDefinition<T> filter)
        {
            var result = new Result<T>();

            try
            {
                result.Data = await _mongoCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<T>> GetItemAsync(string id)
        {
            var result = new Result<T>();

            try
            {
                result.Data = await _mongoCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> AddItemAsync(T newUserInfo)
        {
            var result = new Result();

            try
            {
                await _mongoCollection.InsertOneAsync(newUserInfo);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> UpdateItemAsync(T updatedUserInfo)
        {
            var result = new Result();

            try
            {
                await _mongoCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(updatedUserInfo.Id)), updatedUserInfo);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> RemoveItemAsync (string id)
        {
            var result = new Result();

            try
            {
                await _mongoCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
