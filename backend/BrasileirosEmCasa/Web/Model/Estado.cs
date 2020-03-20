using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Estado
    {
        public virtual int Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual String UF { get; set; }

        public virtual IList<Cidade> Cidades { get; set; }
    }
}
