using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ConfigurationSettings
    {
        private ISessionFactory _sessionFactory;
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public void CreateDatabase()
        {

            var sqlConfiguration = MsSqlConfiguration.MsSql2012
                   .Driver<MicrosoftDataSqlClientDriver>()
                   .ConnectionString(connectionString)
                   .ShowSql();

            var configuration = Fluently.Configure()
                    .Database(sqlConfiguration)
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProcedureDefinitionMapping>())
                    .BuildConfiguration();

            bool tableExists;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ProcedureDefinition'";
                tableExists = (int)command.ExecuteScalar() > 0;
            }

            if (!tableExists)
            {
                var exporter = new SchemaExport(configuration);
                exporter.Execute(true, true, false);
            }

            _sessionFactory = configuration.BuildSessionFactory();
   
        }

        public ISessionFactory SessionFactory()
        {
            return _sessionFactory;
        }
    }
}
