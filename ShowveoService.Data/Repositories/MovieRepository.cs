using System;
using System.Linq;
using NHibernate.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// A container for movie information.
	/// </summary>
	public class MovieRepository : Repository, IMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Inserts a movie into the repository.
		/// </summary>
		/// <param name="movie">The movie to insert.</param>
		public void Insert(Movie movie)
		{
			if (movie == null)
				throw new ArgumentNullException("movie");

			CurrentSession.Save(movie);
		}

		/// <summary>
		/// Retrieves a collection of all movies.
		/// </summary>
		/// <returns>The collection of all movies.</returns>
		public IQueryable<Movie> GetAll()
		{
			return CurrentSession.Query<Movie>().Fetch(x => x.Genres).Fetch(x => x.Actors);
		}
		#endregion
	}
}