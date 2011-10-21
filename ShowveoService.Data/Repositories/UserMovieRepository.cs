using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// A container for user-movie information.
	/// </summary>
	public class UserMovieRepository : Repository, IUserMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of user-movie objects for a user.
		/// </summary>
		/// <param name="user">The user for which user-movie objects should be retrieved.</param>
		/// <returns>The collection of user-movie objects.</returns>
		public IEnumerable<UserMovie> GetForUser(User user)
		{
			if (user == null)
				throw new ArgumentNullException("user");

			return CurrentSession.Query<UserMovie>().Where(x => x.User == user);
		}

		/// <summary>
		/// Inserts a user-movie object into the repository.
		/// </summary>
		/// <param name="userMovie">The user-movie object to insert.</param>
		public void Insert(UserMovie userMovie)
		{
			if (userMovie == null)
				throw new ArgumentNullException("userMovie");
			if (userMovie.User == null)
				throw new ArgumentNullException("user");
			if (userMovie.Movie == null)
				throw new ArgumentNullException("movie");

			CurrentSession.Save(userMovie);
		}
		#endregion
	}
}