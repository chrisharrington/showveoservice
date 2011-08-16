using NHibernate;

namespace Showveo.Data.Repositories
{
	/// <summary>
	/// The base NHibernate repository allowing the implementer to reference opened sessions.
	/// </summary>
	public class Repository
	{
		#region Properties
		/// <summary>
		/// The session opened at the beginning of the request.
		/// </summary>
		protected ISession CurrentSession { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		public Repository()
		{
			CurrentSession = SessionFactoryProvider.Factory.GetCurrentSession();
		}
		#endregion
	}
}