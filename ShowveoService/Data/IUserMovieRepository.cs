using System.Collections.Generic;
using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for user-movie information.
	/// </summary>
	public interface IUserMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of user-movie objects for a user.
		/// </summary>
		/// <param name="user">The user for which user-movie objects should be retrieved.</param>
		/// <returns>The collection of user-movie objects.</returns>
		IEnumerable<UserMovie> GetForUser(User user);

		/// <summary>
		/// Inserts a user-movie object into the repository.
		/// </summary>
		/// <param name="userMovie">The user-movie object to insert.</param>
		void Insert(UserMovie userMovie);
		#endregion
	}
}