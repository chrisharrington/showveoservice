using System;
using System.Linq;
using NHibernate.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// A container for uncategorized movie information.
	/// </summary>
	public class UncategorizedMovieRepository : Repository, IUncategorizedMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of uncategorized movie information.
		/// </summary>
		/// <returns>The retrieved uncategorized movie information.</returns>
		public IQueryable<UncategorizedMovie> GetAll()
		{
			return CurrentSession.Query<UncategorizedMovie>();
		}

		/// <summary>
		/// Inserts an uncategorized movie into the repository.
		/// </summary>
		/// <param name="movie">The uncategorized movie to insert.</param>
		public void Insert(UncategorizedMovie movie)
		{
			if (movie == null)
				throw new ArgumentNullException("movie");

			CurrentSession.Save(movie);
		}
		#endregion
	}
}