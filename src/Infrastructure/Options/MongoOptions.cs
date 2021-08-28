using Infrastructure.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Options
{
    public class MongoOptions
    {

        public const string Name = "MongoOption";

        public string ConnectionString { get; set; }
        public string CollectionPrefix { get; set; }
        public string MenuItemCollectionName { get; set; }
        public string UserInfoCollectionName { get; set; }
        public string UserAccountCollectionName { get; set; }

        public string this[MongoCollection collection]
        {
            get
            {
                var result = CollectionPrefix;
                switch (collection)
                {
                    case MongoCollection.MenuItem: result += MenuItemCollectionName; break;
                    case MongoCollection.UserAccount: result += UserAccountCollectionName; break;
                    case MongoCollection.UserInfo: result += UserInfoCollectionName; break;
                    default: return null;
                }
                return result;
            }
        }
    }
}
