using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Repository.Mapping
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Table("Pessoa");
            Id(x => x.Id).GeneratedBy.Guid();
            Map(x => x.QuantidadePessoasCasa).Column("QuantidadePessoasEmCasa").Not.Nullable();
            Map(x => x.DataInicioQuarentena).Not.Nullable();

            References(x => x.Cidade).Unique().Column("IdCidade").LazyLoad();
        }


    }
}
