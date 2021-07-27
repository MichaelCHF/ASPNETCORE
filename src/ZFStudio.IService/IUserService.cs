using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFStudio.Models;

namespace ZFStudio.IService
{
    public interface IUserService : IBaseService<UserInfo>
    {
        Task<bool> Login(string userId,string password);

        UserInfo GetUser(string userId);

        //UserInfo AddUser(UserInfo userInfo);
    }
}
