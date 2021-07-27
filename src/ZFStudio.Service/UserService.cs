using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFStudio.Data;
using ZFStudio.IService;
using ZFStudio.Models;

namespace ZFStudio.Service
{
    public class UserService : BaseService<UserInfo>, IUserService
    {
        private readonly MyDbContext _dbContext = null;
        public UserService(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UserInfo GetUser(string userId)
        {
            return this.Query(o => o.UserId == userId);
        }

        public async Task<bool> Login(string userId, string password)
        {
            bool flag = false;
            var user = await this.QueryByKeyAsync(userId);
            if (user != null)
            {
                if (user.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase))
                {
                    flag = true;
                }
            }

            return flag;
        }
    }
}
