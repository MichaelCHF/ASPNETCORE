using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZFStudio.Data;
using ZFStudio.IService;

namespace ZFStudio.Service
{
    public class BaseService<T> : IDisposable, IBaseService<T> where T : class, new()
    {
        private readonly MyDbContext _dbContext = null;

        public BaseService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            return await _dbContext.SaveChangesAsync();
        }
        

        public async Task<int> EditAsync(T t)
        {
            _dbContext.Update<T>(t);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            T t = await this.QueryByKeyAsync(id);
            if (t != null)
            {
                _dbContext.Set<T>().Remove(t);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("记录不存在，无法删除");
            }
        }

        public async Task<T> QueryByKeyAsync(object keyValue) => await _dbContext.Set<T>().FindAsync(keyValue);//通过主键查找

        public T Query(Expression<Func<T,bool>> express) => _dbContext.Set<T>().FirstOrDefault<T>(express);

        public IQueryable<T> Query(int pageIndex = 0, int pageSize = 10)
        {
            return _dbContext.Set<T>().Skip(pageIndex * pageSize).Take(pageSize);
        }

        

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

    }
}
