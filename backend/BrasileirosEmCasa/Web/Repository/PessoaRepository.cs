using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Database;
using Web.Model;

namespace Web.Repository
{
    public class PessoaRepository : UnitOfWork<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(ISession session) : base(session)
        {
        }
    }
}
