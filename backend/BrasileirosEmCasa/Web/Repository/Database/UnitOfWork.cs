using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Database
{
    public class UnitOfWork<T> : IDisposable, ISession<T> where T: class
    {
        public IQueryable<T> Query { get; set; }
        public ISession Session { get; set; }

        public UnitOfWork(ISession session)
        {
            this.Session = session;

            this.Query = this.Session.Query<T>();
        }

        public ITransaction CreateTransaction()
        {
            return this.Session.BeginTransaction();
        }

        public ITransaction CreateTransaction(System.Data.IsolationLevel isolation = System.Data.IsolationLevel.Serializable)
        {
            return this.Session.BeginTransaction(isolation);
        }

        public async Task Delete(T entity)
        {
            await this.Session.DeleteAsync(entity);
        }

        public async Task Save(T entity)
        {
            await this.Session.SaveOrUpdateAsync(entity);
        }

        public async Task Update(T entity)
        {
            await this.Session.UpdateAsync(entity);
        }

        public async Task Merge(T entity)
        {
            await this.Session.MergeAsync(entity);
        }


        public async Task Evict(T entity)
        {
            await this.Session.EvictAsync(entity);
        }

        public async Task<T> Get(object id)
        {
            return await this.Session.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.Query.ToListAsync();
        }

        public void SaveChanges()
        {
            this.Session.Flush();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                    if (Session.Transaction != null && Session.Transaction.IsActive)
                        Session.Transaction.Commit();

                    if (Session.IsOpen)
                        Session.Close();

                    

                    Session.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
       
        #endregion

    }
}
