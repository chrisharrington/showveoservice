using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using ShowveoService.Data.Maps;

namespace ShowveoService.Data
{
	/// <summary>
	/// A class used to manage sessions.
	/// </summary>
	public static class SessionProvider
	{
		#region Data Members
		/// <summary>
		/// The factory used to create sessions.
		/// </summary>
		private static readonly ISessionFactory _factory;

		/// <summary>
		/// The session to use in the event no http context exists.
		/// </summary>
		private static ISession _session;
		#endregion

		#region Properties
		/// <summary>
		/// Returns the currently active session.
		/// </summary>
		public static ISession CurrentSession
		{
			set { _session = value; }
			get
			{
				if (HttpContext.Current == null)
					return _session ?? (_session = _factory.OpenSession());
				return HttpContext.Current.Items["currentsession"] as ISession;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		static SessionProvider()
		{
			_factory = Fluently
				.Configure()
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
				.BuildSessionFactory();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Binds a session to a context.
		/// </summary>
		/// <param name="context">The http context.</param>
		public static void Open(HttpContext context)
		{
			var session = _factory.OpenSession();
			session.BeginTransaction();
			if (context != null)
				context.Items["currentsession"] = session;
		}

		/// <summary>
		/// Unbinds a session from an http context.
		/// </summary>
		/// <param name="context">The http context.</param>
		public static void Close(HttpContext context)
		{
			if (context == null)
				return;

			var session = context.Items["currentsession"] as ISession;
			if (session == null)
				return;

			if (session.Transaction != null)
				session.Transaction.Commit();
			session.Close();
		}
		#endregion
	}
}