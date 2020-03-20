using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Repository.Mapping
{
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nome).Not.Nullable();
            Map(x => x.UF).Not.Nullable();
            HasMany<Cidade>(x => x.Cidades).KeyColumn("uf").LazyLoad().Inverse();
        }
    }
}
