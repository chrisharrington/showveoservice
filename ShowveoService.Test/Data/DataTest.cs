using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using ShowveoService.Data.Maps;

namespace ShowveoService.Test.Data
{
	/// <summary>
	/// A base test class that provides access to an in-memory database.
	/// </summary>
	[TestFixture]
	public class DataTest
	{
		#region Data Members
		/// <summary>
		/// The underlying in-memory session.
		/// </summary>
		private ISession _inMemorySession;
		#endregion

		#region Properties
		/// <summary>
		/// The session used to access the in-memory database.
		/// </summary>
		protected ISession InMemorySession
		{
			get
			{
				if (_inMemorySession == null)
					CreateInMemorySession();
				return _inMemorySession;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Creates the in-memory session.
		/// </summary>
		[SetUp]
		public void BaseSetup()
		{
			CreateInMemorySession();
		}

		/// <summary>
		/// Tears down the tests.
		/// </summary>
		[TearDown]
		public void BaseTearDown()
		{
			if (_inMemorySession != null)
				_inMemorySession.Close();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Creates the in-memory session.
		/// </summary>
		private void CreateInMemorySession()
		{
			Configuration configuration = null;
			var factory = Fluently
				.Configure()
				.ExposeConfiguration(x => configuration = x)
				.Database(SQLiteConfiguration.Standard.InMemory())
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
				.BuildSessionFactory();

			_inMemorySession = factory.OpenSession();
			new SchemaExport(configuration).Execute(false, true, false, _inMemorySession.Connection, null);
		}
		#endregion
	}
}