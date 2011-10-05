using System.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for movie information.
	/// </summary>
	public interface IMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Inserts a movie into the repository.
		/// </summary>
		/// <param name="movie">The movie to insert.</param>
		void Insert(Movie movie);

		/// <summary>
		/// Retrieves a collection of all movies.
		/// </summary>
		/// <returns>The collection of all movies.</returns>
		IQueryable<Movie> GetAll();
		#endregion
	}
}