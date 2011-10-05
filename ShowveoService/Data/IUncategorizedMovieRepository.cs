using System.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for uncategorized movie information.
	/// </summary>
	public interface IUncategorizedMovieRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of uncategorized movie information.
		/// </summary>
		/// <returns>The retrieved uncategorized movie information.</returns>
		IQueryable<UncategorizedMovie> GetAll();

		/// <summary>
		/// Inserts an uncategorized movie into the repository.
		/// </summary>
		/// <param name="movie">The uncategorized movie to insert.</param>
		void Insert(UncategorizedMovie movie);
		#endregion
	}
}