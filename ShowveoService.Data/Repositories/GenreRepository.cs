using System;
using System.Linq;
using NHibernate.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// A container for genre information.
	/// </summary>
	public class GenreRepository : Repository, IGenreRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of all genres.
		/// </summary>
		/// <returns></returns>
		public IQueryable<Genre> GetAll()
		{
			return CurrentSession.Query<Genre>();
		}

		/// <summary>
		/// Inserts a genre into the repository.
		/// </summary>
		/// <param name="genre">The genre to insert.</param>
		public void Insert(Genre genre)
		{
			if (genre == null)
				throw new ArgumentNullException("genre");

			CurrentSession.Save(genre);
		}
		#endregion
	}
}