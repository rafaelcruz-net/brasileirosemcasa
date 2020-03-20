using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Database
{
    public interface IRepository<T>
    {
        Task Save(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        IQueryable<T> Query { get; }
        Task<T> Get(object id);
        Task<IEnumerable<T>> GetAll();
        ITransaction CreateTransaction();
        ITransaction CreateTransaction(System.Data.IsolationLevel isolation = System.Data.IsolationLevel.Serializable);
        void SaveChanges();
        Task Evict(T entity);
        Task Merge(T entity);
    }
}
