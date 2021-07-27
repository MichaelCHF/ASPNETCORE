using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ZFStudio.IService
{
    public interface IBaseService<T> where T : class, new()
    {
        Task<int> CreateAsync(T t);

        Task<int> EditAsync(T t);

        Task<int> RemoveAsync(int i);

        Task<T> QueryByKeyAsync(object key);

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public T Query(Expression<Func<T, bool>> express);

        IQueryable<T> Query(int pageIndex = 0, int pageSize = 10);

    }
}
