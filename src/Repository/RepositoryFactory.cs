using System;

using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Models.User;
using Infrastructure.Options;

using RepositoryService.Interfaces;
using RepositoryService.Mongo;

namespace RepositoryService
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T>(MongoOptions mongoOption = null)
        {
            if (typeof(T) == typeof(MenuItem))
            {
                return (IRepository<T>)new MenuItemMongoRepository(mongoOption);
            }
            if (typeof(T) == typeof(UserInfo))
            {
                return (IRepository<T>)new CommonMongoRepository<UserInfo>(mongoOption, MongoCollection.UserInfo);
            }

            throw new InvalidOperationException();
        }
    }
}
