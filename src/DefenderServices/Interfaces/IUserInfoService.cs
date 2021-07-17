using Infrastructure.Models.User;
using Infrastructure.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefenderServices.Interfaces
{
    public interface IUserInfoService
    {
        Task<IResultWithData<IList<UserInfo>>> GetUsers();

        Task<IResultWithData<UserInfo>> GetUserById(int id);

        Task<IResult> CreateUser(UserInfo newUserInfo);

        Task<IResult> UpdateUserInfo(UserInfo newUserInfo);

        Task<IResult> RemoveUser(int id);
    }
}
