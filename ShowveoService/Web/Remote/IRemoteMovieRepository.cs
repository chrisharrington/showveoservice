using System.Collections.Generic;
using ShowveoService.Entities;

namespace ShowveoService.Web.Remote
{
	/// <summary>
	/// Defines a container for movie information hosted remotely.
	/// </summary>
	public interface IRemoteMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Performs a search for movie information.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <returns>The resulting movie information.</returns>
		IEnumerable<RemoteMovie> Search(string query);

		/// <summary>
		/// Retrieves details for a movie.
		/// </summary>
		/// <param name="id">The movie ID.</param>
		/// <returns>The retrieved movie details.</returns>
		Movie GetDetails(int id);
		#endregion
	}
}