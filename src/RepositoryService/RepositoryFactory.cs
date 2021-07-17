using Infrastructure.Models;
using Infrastructure.Models.User;
using RepositoryService.Interfaces;
using System;

namespace RepositoryService
{
    public static class RepositoryFactory
    {
        public static IJSONRepository<T> CreateRepository<T>()
        {
            if (typeof(T) == typeof(MenuItem))
            {
                return (IJSONRepository<T>)new MenuItemJSONRepository();
            }
            if (typeof(T) == typeof(UserInfo))
            {
                return (IJSONRepository<T>)new UserJSONRepository();
            }

            throw new InvalidOperationException();
        }
    }
}
