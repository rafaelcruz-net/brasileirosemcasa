using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Web.Database
{
    public static class SessionFactory
    {
        public static ISessionFactory CreateSessionFactory(string connectionString, Assembly assembly)
        {
            return Fluently.Configure()
                           .Database(() =>
                           {
                               return FluentNHibernate.Cfg.Db.MsSqlConfiguration
                                                             .MsSql2012
#if DEBUG
                                                              .ShowSql()
#endif
                                                             .ConnectionString(connectionString);
                                                            
                           })
                           .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                           .ExposeConfiguration(c => 
                           {
                               c.SetProperty("default_flush_mode", "Commit");
                               #if DEBUG
                               c.SetInterceptor(new SqlStatementInterceptor());
                               #endif
                           })
                           .BuildSessionFactory();
        }

        public class SqlStatementInterceptor : EmptyInterceptor
        {
            public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
            {
                Trace.WriteLine(base.OnPrepareStatement(sql).ToString());
                Console.WriteLine(base.OnPrepareStatement(sql).ToString());
                return sql;
            }
        }
    }
}
