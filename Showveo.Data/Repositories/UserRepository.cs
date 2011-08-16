using System;
using System.Linq;
using NHibernate.Linq;
using Showveo.Entities;

namespace Showveo.Data.Repositories
{
	/// <summary>
	/// A container for user information.
	/// </summary>
	public class UserRepository : Repository, IUserRepository
	{
		#region Public Methods
		/// <summary>
		/// Attempts to authenticate a user. Null is returned if the authentication fails.
		/// </summary>
		/// <param name="emailAddress">The potential user's email address.</param>
		/// <param name="password">The potential user's encrypted password.</param>
		/// <returns>The authenticated user or null.</returns>
		public User Authenticate(string emailAddress, string password)
		{
			if (string.IsNullOrEmpty(emailAddress))
				throw new ArgumentNullException("emailAddress");
			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");

			return CurrentSession.Query<User>().Where(x => x.EmailAddress == emailAddress && x.Password == password).FirstOrDefault();
		}
		#endregion
	}
}