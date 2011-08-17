using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// Creates and stores session factories.
	/// </summary>
	public static class SessionFactoryProvider
	{
		#region Properties
		/// <summary>
		/// Retrieves the currently mapped factory.
		/// </summary>
		public static ISessionFactory Factory { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		static SessionFactoryProvider()
		{
			Configuration configuration = null;
			Factory = Fluently
				.Configure()
				.ExposeConfiguration(x => configuration = x)
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<User>())
				.BuildSessionFactory();

			if (configuration == null)
				return;

			new SchemaExport(configuration).Execute(false, true, false);
		}
		#endregion
	}
}