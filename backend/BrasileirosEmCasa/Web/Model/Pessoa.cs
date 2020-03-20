using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Pessoa
    {
        public virtual Guid Id { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual DateTime DataInicioQuarentena { get; set; }
        public virtual int QuantidadePessoasCasa { get; set; }
    }
}
