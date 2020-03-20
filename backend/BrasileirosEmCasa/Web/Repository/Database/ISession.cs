using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Database
{
    public interface ISession<T> where T : class
    {
        ITransaction CreateTransaction();
        ITransaction CreateTransaction(System.Data.IsolationLevel isolation);
        void SaveChanges();

        Task Evict(T entity);


    }
}
