using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

using Infrastructure.Results;
using System.Linq;
using Infrastructure.Models.User;

namespace RepositoryService
{
    public class UserJSONRepository : BaseJSONRepository<UserInfo>
    {
        public override async Task<IResultWithData<IList<UserInfo>>> GetItemsAsync()
        {
            var result = new Result<IList<UserInfo>>();

            try
            {
                var jsonString = await File.ReadAllTextAsync(_userInfoRepositoryPath);
                var usersInfo = JsonSerializer.Deserialize<List<UserInfo>>(jsonString);
                result.Data = usersInfo;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResultWithData<UserInfo>> GetItemAsync(int id)
        {
            var usersInfoResult = await GetItemsAsync();
            var result = new Result<UserInfo>();

            if (!usersInfoResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read user info";
                return result;
            }

            result.Data = usersInfoResult.GetData.FirstOrDefault(x => x.ID == id);

            return result;
        }

        public override async Task<IResult> AddItemAsync(UserInfo newUserInfo)
        {
            var usersInfoResult = await GetItemsAsync();
            var result = new Result();

            if (!usersInfoResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read user info";
                return result;
            }

            try
            {
                var usersInfo = usersInfoResult.GetData;
                newUserInfo.ID = FindMinimumFreeId(usersInfo);
                usersInfo.Add(newUserInfo);
                usersInfo = SortById(usersInfo);
                var jsonString = JsonSerializer.Serialize(usersInfo);
                File.WriteAllText(_userInfoRepositoryPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> UpdateItemAsync(UserInfo updatedUserInfo)
        {
            var usersInfoResult = await GetItemsAsync();
            var result = new Result();

            if (!usersInfoResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read user info";
                return result;
            }

            try
            {
                var usersInfo = usersInfoResult.GetData;
                var itemToUpdate = usersInfo.First(x => x.ID == updatedUserInfo.ID);
                itemToUpdate.UserName = updatedUserInfo.UserName;
                itemToUpdate.PasswordFolderPath = updatedUserInfo.PasswordFolderPath;
                var jsonString = JsonSerializer.Serialize(usersInfo);
                File.WriteAllText(_userInfoRepositoryPath, jsonString);
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.Error;
                result.Message = e.Message;
            }

            return result;
        }

        public override async Task<IResult> RemoveItemAsync (int id)
        {
            var usersInfoResult = await GetItemsAsync();
            var result = new Result();

            if (!usersInfoResult.IsSuccess)
            {
                result.Status = ResultStatus.Error;
                result.Message = "Error when read user info";
                return result;
            }

            try
            {
                var usersInfo = usersInfoResult.GetData;
                var itemToUpdate = usersInfo.Remove(usersInfo.First(x => x.ID == id));
                var jsonString = JsonSerializer.Serialize(usersInfo);
                File.WriteAllText(_userInfoRepositoryPath, jsonString);
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
