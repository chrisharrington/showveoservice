using NHibernate;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// The base NHibernate repository allowing the implementer to reference opened sessions.
	/// </summary>
	public class Repository
	{
		#region Data Members
		/// <summary>
		/// The session used to communicate with the database.
		/// </summary>
		private ISession _session;
		#endregion

		#region Properties
		/// <summary>
		/// The session opened at the beginning of the request.
		/// </summary>
		protected ISession CurrentSession
		{
			get { return _session ?? (_session = SessionProvider.CurrentSession); }
			set { _session = value; }
		}
		#endregion
	}
}