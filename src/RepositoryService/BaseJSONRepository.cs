using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Infrastructure.Results;
using RepositoryService.Interfaces;
using Infrastructure.Models;
using System.Linq;

namespace RepositoryService
{
    public class BaseJSONRepository<Model> : IJSONRepository<Model> where Model : BaseModel
    {
        protected readonly string _menuItemRepositoryPath = "TempDatabase/menuitems.json";
        protected readonly string _userInfoRepositoryPath = "TempDatabase/userinfo.json";

        public BaseJSONRepository()
        {
        }

        public virtual Task<IResult> AddItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<Model>> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResultWithData<IList<Model>>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResult> RemoveItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IResult> UpdateItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        protected int FindMinimumFreeId(IList<Model> models)
        {
            var newId = models.Count;
            var last = -1;

            for (int i = 0; i < models.Count; i++)
            {
                if (last + 1 != models[i].ID)
                {
                    return i;
                }
                last++;
            }

            return newId;
        }

        protected IList<Model> SortById(IList<Model> unsortedList)
        {
            var sortedList = unsortedList.OrderBy(x => x.ID);
            return sortedList.ToList();
        }
    }

    public class BaseJSONRepository : BaseJSONRepository<BaseModel>
    {

    }
}
