using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using ShowveoService.Data.Maps;
using ShowveoService.Entities;
using ShowveoService.MVCApplication.Load;

namespace ShowveoService.Test
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void Blah()
		{
			Loader.Start();

			//var repository = DR.Get<IUserRepository>();
			//var user = repository.Authenticate("blah", "boo");

			Configuration configuration = null;
			var factory = Fluently
				.Configure()
				.ExposeConfiguration(x => configuration = x)
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
				.BuildSessionFactory();

			if (configuration == null)
				return;

			new SchemaExport(configuration).Execute(false, true, false);

			IEnumerable<User> users = null;
			using (var session = factory.OpenSession())
			{
				users = session.Query<User>().ToArray();
			}
		}
	}
}