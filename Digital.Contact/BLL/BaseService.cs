using Digital.Contact.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public interface IBaseService<TEntity>
    {
        IList<TEntity> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount);
        IList<TEntity> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount,
            Func<TEntity, bool> whereLambda, bool isAsc, Func<TEntity, S> orderByLambda);

        TEntity Find(int? Id);

        bool Edit(TEntity model);

        bool Delete(int Id);
    }
}
