using System.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for genre information.
	/// </summary>
	public interface IGenreRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of all genres.
		/// </summary>
		/// <returns></returns>
		IQueryable<Genre> GetAll();

		/// <summary>
		/// Inserts a genre into the repository.
		/// </summary>
		/// <param name="genre">The genre to insert.</param>
		void Insert(Genre genre);
		#endregion
	}
}