using System.Collections.Generic;
using System.Threading.Tasks;
using DefenderServices.Interfaces;
using Infrastructure.Models.User;
using Infrastructure.Results;
using RepositoryService;
using RepositoryService.Interfaces;

namespace DefenderServices
{
    public class UserInfoService : IUserInfoService
    {
        private IRepository<UserInfo> _userInfoRepository;

        public UserInfoService()
        {
            _userInfoRepository = RepositoryFactory.CreateRepository<UserInfo>();
        }

        #region CRUD
        public async Task<IResult> CreateUser(UserInfo newUserInfo)
        {
            var userInfoResult = await _userInfoRepository.AddItemAsync(newUserInfo);

            return userInfoResult;
        }

        public async Task<IResultWithData<UserInfo>> GetUserById(string id)
        {
            var userInfoResult = await _userInfoRepository.GetItemAsync(id);

            return userInfoResult;
        }

        public async Task<IResultWithData<IList<UserInfo>>> GetUsers()
        {
            var usersInfoResult = await _userInfoRepository.GetItemsAsync();

            return usersInfoResult;
        }

        public async Task<IResult> RemoveUser(string id)
        {
            var userInfoResult = await _userInfoRepository.RemoveItemAsync(id);

            return userInfoResult;
        }

        public async Task<IResult> UpdateUserInfo(UserInfo newUserInfo)
        {
            var userInfoResult = await _userInfoRepository.UpdateItemAsync(newUserInfo);

            return userInfoResult;
        }
        #endregion
    }
}
