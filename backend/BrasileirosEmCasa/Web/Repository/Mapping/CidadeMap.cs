using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Repository.Mapping
{
    public class CidadeMap : ClassMap<Cidade>
    {
        public CidadeMap()
        {
            Table("Cidade");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nome);
            References(x => x.Estado).Unique().Column("uf").LazyLoad();
        }
    }
}
