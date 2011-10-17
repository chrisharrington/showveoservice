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

		/// <summary>
		/// Removes an uncategorized movie from the repository.
		/// </summary>
		/// <param name="id">The ID of the uncategorized movie to remove.</param>
		public void Remove(int id)
		{
			if (id < 1)
				throw new ArgumentOutOfRangeException("id");

			var movie = CurrentSession.Query<UncategorizedMovie>().Where(x => x.ID == id).FirstOrDefault();
			if (movie == null)
				return;

			CurrentSession.Delete(movie);
		}
		#endregion
	}
}