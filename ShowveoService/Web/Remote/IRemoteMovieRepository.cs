using System.Collections.Generic;
using ShowveoService.Entities;

namespace ShowveoService.Web
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
		#endregion
	}
}