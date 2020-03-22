using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Repository
{
    public interface ICovid19Repository
    {
        Task<IEnumerable<Covid19PerState>> GetCovidInBrazilPerState();
    }
}
