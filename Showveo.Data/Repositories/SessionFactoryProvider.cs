using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Showveo.Entities;

namespace Showveo.Data.Repositories
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
			Factory = Fluently
				.Configure()
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<User>())
				.BuildSessionFactory();
		}
		#endregion
	}
}